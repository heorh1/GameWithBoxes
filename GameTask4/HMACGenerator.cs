using System.Text;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;

namespace GameTask4
{
    public static class HMACGenerator
    {
        public static string GenerateHMAC(string message, byte[] key)
        {
            var hmac = new HMac(new Sha3Digest(256));
            hmac.Init(new KeyParameter(key));

            byte[] msgBytes = Encoding.UTF8.GetBytes(message);
            hmac.BlockUpdate(msgBytes, 0, msgBytes.Length);

            byte[] result = new byte[hmac.GetMacSize()];
            hmac.DoFinal(result, 0);

            return Hex.ToHexString(result);
        }
    }
}
