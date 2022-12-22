// ReSharper disable once CheckNamespace

namespace Kinetic.Sdk.Tests
{

    public static class KineticSdkFixture
    {

        public static readonly byte[] CreateAccountCompiledTransaction =
        {
            2, 1, 5, 8, 5, 84, 102, 38, 7, 100, 200, 251, 34, 72, 143, 134, 254, 202, 250, 134, 208, 29, 170, 161, 12,
            132, 240, 98, 16, 241, 4, 69, 193, 117, 148, 215, 217, 155, 131, 107, 150, 111, 72, 99, 95, 128, 23, 112,
            107, 118, 138, 186, 66, 75, 159, 87, 12, 187, 205, 150, 201, 142, 157, 239, 138, 140, 153, 112, 13, 28, 183,
            123, 9, 74, 72, 212, 242, 114, 210, 52, 214, 12, 181, 130, 62, 234, 91, 65, 113, 191, 113, 10, 131, 114,
            144, 90, 46, 187, 28, 229, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 140, 151, 37, 143, 78, 36, 137, 241, 187, 61, 16, 41, 20, 142, 13, 131, 11, 90, 19, 153, 218,
            255, 16, 132, 4, 142, 123, 216, 219, 233, 248, 89, 4, 203, 177, 231, 50, 196, 176, 149, 105, 20, 234, 249,
            2, 87, 247, 98, 185, 42, 178, 83, 248, 34, 205, 159, 214, 224, 55, 133, 9, 93, 219, 12, 6, 167, 213, 23, 25,
            44, 92, 81, 33, 140, 201, 76, 61, 74, 241, 127, 88, 218, 238, 8, 155, 161, 253, 68, 227, 219, 217, 138, 0,
            0, 0, 0, 6, 221, 246, 225, 215, 101, 161, 147, 217, 203, 225, 70, 206, 235, 121, 172, 28, 180, 133, 237, 95,
            91, 55, 145, 58, 140, 245, 133, 126, 255, 0, 169, 149, 202, 146, 171, 21, 31, 2, 128, 230, 222, 43, 142,
            114, 36, 210, 211, 53, 188, 181, 82, 81, 243, 0, 231, 45, 222, 104, 7, 113, 222, 92, 238, 2, 4, 7, 0, 2, 1,
            5, 3, 7, 6, 0, 7, 2, 2, 1, 35, 6, 3, 1, 5, 84, 102, 38, 7, 100, 200, 251, 34, 72, 143, 134, 254, 202, 250,
            134, 208, 29, 170, 161, 12, 132, 240, 98, 16, 241, 4, 69, 193, 117, 148, 215
        };

        public static readonly byte[] CreateAccountPartialSignature =
        {
            108, 50, 94, 175, 55, 107, 211, 168, 41, 145, 115, 48, 8, 33, 122, 226, 49, 233, 131, 76, 21, 211, 43, 61,
            97, 183, 156, 226, 201, 41, 182, 121, 217, 250, 238, 76, 4, 19, 24, 196, 157, 151, 211, 203, 168, 71, 45,
            124, 138, 232, 252, 130, 3, 120, 6, 92, 109, 6, 93, 143, 36, 18, 138, 11
        };
        
        public static readonly Keypair CreateAccountSigner = Keypair.FromSecretKey(
            "5icCMHSkDzPFzY4UxHRP67BsTq8EcA6nVSL2ifpzsveGa79G3d8gU8hmWxhopR16s5WgBnFS7J6upt5ZevhihQ3q");

        // ALisrzsaVqciCxy8r6g7MUrPoRo3CpGxPhwBbZzqZ9bA
        private static readonly byte[] AliceKey =
        {

            205, 213, 7, 246, 167, 206, 37, 209, 161, 129, 168, 160, 90, 103, 198, 142, 83, 177, 214, 203, 80, 29,
            71, 245, 56, 152, 15, 8, 235, 174, 62, 79, 138, 198, 145, 111, 119, 33, 15, 237, 89, 201, 122, 89, 48, 221,
            224, 71, 81, 128, 45, 97, 191, 105, 37, 228, 243, 238, 130, 151, 53, 221, 172, 125,
        };

        // DAVEXJQuNAzUaVZsFeDiUr67WikDH3cj4L1YHyx5t2Nj
        private static readonly byte[] DaveKey =
        {
            99, 184, 26, 205, 22, 184, 129, 159, 152, 181, 28, 169, 124, 74, 249, 50, 233, 159, 85, 86, 195, 237, 252,
            22, 140, 73, 253, 235, 97, 243, 180, 21, 180, 186, 100, 202, 161, 4, 96, 215, 244, 173, 174, 184, 79, 108,
            59, 85, 0, 114, 203, 238, 237, 212, 159, 220, 81, 103, 142, 111, 199, 18, 74, 168,
        };
        
        // BobQoPqWy5cpFioy1dMTYqNH9WpC39mkAEDJWXECoJ9y
        private static readonly byte[] BobKey =
        {
            18, 115, 33, 33, 38, 237, 120, 11, 143, 149, 233, 210, 110, 63, 168, 30, 222, 58, 179, 219, 72, 97, 178,
            234, 96, 253, 41, 76, 58, 178, 196, 71, 160, 132, 126, 143, 172, 202, 194, 16, 114, 76, 228, 235, 255, 88,
            90, 195, 224, 74, 167, 48, 234, 92, 160, 3, 29, 163, 62, 235, 147, 240, 30, 108,
        };

        // FIXME: This should be MoGaMuJnB3k8zXjBYBnHxHG47vWcW3nyb7bFYvdVzek once we switch tests to localhost:3000
        public const string DefaultMint = "KinDesK3dYWo3R2wDk6Ucaf31tvQCCSYyL8Fuqp33GX";
        public static readonly Keypair AliceKeypair = Keypair.FromByteArray(AliceKey);
        // FIXME: Change this to the PDA from Alice and 'MoGa...Vzek' once we switch tests to localhost:3000
        public const string AliceTokenAccount = "2buHAucDpb3gECUNZwZQpfAJ8hELsvaQrByYBekT7NKk";
        
        public static readonly Keypair DaveKeypair = Keypair.FromByteArray(DaveKey);
        
        public static readonly Keypair BobKeypair = Keypair.FromByteArray(BobKey);
    }
}