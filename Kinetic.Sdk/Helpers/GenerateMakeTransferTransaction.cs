using System;
using System.Collections.Generic;
using Kinetic.Sdk.Interfaces;
using Solnet.Programs;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Models;
using Solnet.Wallet;

// ReSharper disable once CheckNamespace

namespace Kinetic.Sdk.Transactions
{
    public static class GenerateMakeTransferTransaction
    {
        public static Transaction Generate(
            bool addMemo,
            string amount,
            string blockhash,
            string destination,
            int index,
            int mintDecimals,
            string mintFeePayer,
            string mintPublicKey,
            Account owner,
            bool senderCreate,
            TransactionType type
        )
        {
            // Create objects from Response
            var mintKey = new PublicKey(mintPublicKey);
            var feePayerKey = new PublicKey(mintFeePayer);
            var ownerPublicKey = owner.PublicKey;

            // Get TokenAccount from Owner and Destination
            var ownerTokenAccount =
                AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(ownerPublicKey, mintKey);
            var destinationTokenAccount =
                AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(new PublicKey(destination), mintKey);

            // Create Instructions
            var instructions = new List<TransactionInstruction>();

            if (addMemo.Equals(true))
                // Create Memo Instruction for KRE Ingestion - Must be Memo Program v1, not v2
                instructions.Add(MemoHelper.Create(ownerPublicKey, index, type: type));

            if (senderCreate)
                instructions.Add(
                    AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(
                        feePayerKey, new PublicKey(destination), mintKey));

            var amountWithDecimals =
                ulong.Parse(amount) * (ulong)Math.Pow(10, mintDecimals);
            instructions.Add(
                TokenProgram.TransferChecked(
                    ownerTokenAccount,
                    destinationTokenAccount,
                    amountWithDecimals,
                    mintDecimals,
                    ownerPublicKey,
                    mintKey
                )
            );

            // Create Transaction
            var transaction = new Transaction
            {
                RecentBlockHash = blockhash,
                FeePayer = feePayerKey,
                Instructions = instructions,
                Signatures = new List<SignaturePubKeyPair>
                {
                    new SignaturePubKeyPair()
                    {
                        PublicKey = feePayerKey,
                        Signature = new byte[TransactionBuilder.SignatureLength]
                    }
                }
            };


            // Partially sign the transaction
            transaction.PartialSign(owner);

            return transaction;
        }
    }
}