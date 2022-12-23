using System;
using System.Collections.Generic;
using Kinetic.Sdk.Helpers;
using Kinetic.Sdk.Interfaces;
using Kinetic.Sdk.Kinetic.Api;
using Kinetic.Sdk.Kinetic.Client;
using Kinetic.Sdk.Kinetic.Model;
using Kinetic.Sdk.Transactions;

// ReSharper disable once CheckNamespace

namespace Kinetic.Sdk
{
    internal static class Constants
    {
        public const string Name = "Kinetic Dotnet SDK";
        public const string Version = "1.0.0-rc.12";
    }

    public class KineticSdkInternal
    {
        private readonly AccountApi _accountApi;
        private readonly AirdropApi _airdropApi;
        private readonly AppApi _appApi;

        private readonly KineticSdkConfig _sdkConfig;
        private readonly TransactionApi _transactionApi;

        internal KineticSdkInternal(KineticSdkConfig config)
        {
            _sdkConfig = config;
            var basePath = config.Endpoint;

            var apiClient = new ApiClient(basePath);

            // TODO: Support user provided headers
            apiClient.AddDefaultHeader("kinetic-environment", config.Environment);
            apiClient.AddDefaultHeader("kinetic-index", config.Index.ToString());
            apiClient.AddDefaultHeader("kinetic-user-agent", $"{Constants.Name}@{Constants.Version}");

            _accountApi = new AccountApi(apiClient);
            _airdropApi = new AirdropApi(apiClient);
            _appApi = new AppApi(apiClient);
            _transactionApi = new TransactionApi(apiClient);
        }

        public AppConfig AppConfig { get; private set; }

        #region Core

        public Transaction CloseAccount(
            string account,
            string mint = null,
            string referenceId = null,
            string referenceType = null,
            Commitment? commitment = null
        )
        {
            var appCommitment = GetCommitment(commitment);
            var appConfig = EnsureAppConfig();
            var appMint = GetAppMint(appConfig, mint);

            var request = new CloseAccountRequest
            {
                Account = account,
                Commitment = appCommitment.ToString(),
                Environment = _sdkConfig.Environment,
                Index = _sdkConfig.Index,
                Mint = appMint.PublicKey,
                ReferenceId = referenceId,
                ReferenceType = referenceType,
            };

            return _accountApi.CloseAccount(request);
        }

        public Transaction CreateAccount(
            Keypair owner,
            string mint = null,
            string referenceId = null,
            string referenceType = null,
            Commitment? commitment = null
        )
        {
            var appCommitment = GetCommitment(commitment);
            var appConfig = EnsureAppConfig();
            var appMint = GetAppMint(appConfig, mint);

            var blockhash = GetBlockhash();

            var tx = GenerateCreateAccountTransaction.Generate(
                appMint.AddMemo,
                blockhash.LatestBlockhash,
                _sdkConfig.Index,
                appMint.FeePayer,
                appMint.PublicKey,
                owner
            );

            var request = new CreateAccountRequest
            {
                Commitment = appCommitment.ToString(),
                Environment = _sdkConfig.Environment,
                Index = _sdkConfig.Index,
                LastValidBlockHeight = blockhash.LastValidBlockHeight,
                Mint = appMint.PublicKey,
                ReferenceId = referenceId,
                ReferenceType = referenceType,
                Tx = Convert.ToBase64String(tx.Serialize())
            };

            return _accountApi.CreateAccount(request);
        }
        
        public AccountInfo GetAccountInfo(string account, Commitment? commitment = null)
        {
            var appCommitment = GetCommitment(commitment);
            
            return _accountApi.GetAccountInfo(_sdkConfig.Environment, _sdkConfig.Index, account, appCommitment.ToString());
        }


        public AppConfig GetAppConfig()
        {
            AppConfig = _appApi.GetAppConfig(_sdkConfig.Environment, _sdkConfig.Index);
            return AppConfig;
        }

        public BalanceResponse GetBalance(string account, Commitment? commitment = null)
        {
            var appCommitment = GetCommitment(commitment);

            return _accountApi.GetBalance(
                _sdkConfig.Environment,
                _sdkConfig.Index,
                account,
                appCommitment.ToString()
            );
        }

        public List<HistoryResponse> GetHistory(string account, string mint = null, Commitment? commitment = null)
        {
            var appCommitment = GetCommitment(commitment);
            var appConfig = EnsureAppConfig();
            var appMint = GetAppMint(appConfig, mint);

            return _accountApi.GetHistory(_sdkConfig.Environment, _sdkConfig.Index, account, appMint.PublicKey, appCommitment.ToString());
        }

        public List<string> GetTokenAccounts(string account, string mint = null, Commitment? commitment = null)
        {
            var appCommitment = GetCommitment(commitment);
            var appConfig = EnsureAppConfig();
            var appMint = GetAppMint(appConfig, mint);

            return _accountApi
                .GetTokenAccounts(_sdkConfig.Environment, _sdkConfig.Index, account, appMint.PublicKey, appCommitment.ToString());
        }

        public GetTransactionResponse GetTransaction(string signature, Commitment? commitment = null)
        {
            var appCommitment = GetCommitment(commitment);

            return _transactionApi
                .GetTransaction(_sdkConfig.Environment, _sdkConfig.Index, signature, appCommitment.ToString());
        }

        public Transaction MakeTransfer(
            Keypair owner,
            string amount,
            string destination,
            string mint = null,
            string referenceId = null,
            string referenceType = null,
            bool senderCreate = false,
            TransactionType type = TransactionType.None,
            Commitment? commitment = null
        )
        {
            var appCommitment = GetCommitment(commitment);
            var appConfig = EnsureAppConfig();
            var appMint = GetAppMint(appConfig, mint);

            if (appConfig.Mints.Find(m => m.PublicKey == destination) != null)
            {
                throw new Exception("Transfers to a mint are not allowed.");
            }

            var blockhash = GetBlockhash();

            var account = GetTokenAccounts(destination, appMint.PublicKey, appCommitment);

            if (account.Count == 0 && !senderCreate) throw new Exception("Destination account doesn't exist.");

            var tx = GenerateMakeTransferTransaction.Generate(
                appMint.AddMemo,
                amount,
                blockhash.LatestBlockhash,
                destination,
                _sdkConfig.Index,
                appMint.Decimals,
                appMint.FeePayer,
                appMint.PublicKey,
                owner.Solana,
                account?.Count == 0 && senderCreate,
                type
            );

            var mkTransfer = new MakeTransferRequest
            {
                Commitment = appCommitment.ToString(),
                Environment = _sdkConfig.Environment,
                Index = _sdkConfig.Index,
                LastValidBlockHeight = blockhash.LastValidBlockHeight,
                Mint = appMint.PublicKey,
                ReferenceId = referenceId,
                ReferenceType = referenceType,
                Tx = Convert.ToBase64String(tx.Serialize())
            };

            return _transactionApi.MakeTransfer(mkTransfer);
        }

        public RequestAirdropResponse RequestAirdrop(
            string account,
            string amount,
            string mint = null,
            Commitment? commitment = null
        )
        {
            var appCommitment = GetCommitment(commitment);
            var appConfig = EnsureAppConfig();
            var appMint = GetAppMint(appConfig, mint);

            return _airdropApi
                .RequestAirdrop(
                    new RequestAirdropRequest
                    {
                        Account = account,
                        Amount = amount,
                        Commitment = appCommitment.ToString(),
                        Environment = _sdkConfig.Environment,
                        Index = _sdkConfig.Index,
                        Mint = appMint.PublicKey
                    }
                );
        }

        #endregion


        #region Utils

        private AppConfig EnsureAppConfig()
        {
            if (AppConfig is null) throw new Exception("AppConfig not initialized");
            return AppConfig;
        }
        

        private Commitment GetCommitment(Commitment? commitment = null)
        {
            return commitment ?? _sdkConfig.Commitment ?? Commitment.Confirmed;
        }

        private AppConfigMint GetAppMint(AppConfig appConfig, string mint = null)
        {
            mint ??= AppConfig.Mint.PublicKey;
            var found = appConfig.Mints.Find(m => m.PublicKey == mint);
            if (found == null) throw new Exception("Mint not found");
            return found;
        }

        private PreTransaction GetBlockhash()
        {
            var latestBlockhashResponse =
                _transactionApi.GetLatestBlockhash(_sdkConfig.Environment, _sdkConfig.Index);

            return new PreTransaction
            {
                LatestBlockhash = latestBlockhashResponse.Blockhash,
                LastValidBlockHeight = latestBlockhashResponse.LastValidBlockHeight
            };
        }

        #endregion
    }
}