// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading.Tasks;

// namespace TudoFarmaRep.CrossCutting.Helper
// {
//     public class AesCryptoHelper
//     {
//         private byte[] Key { get; set; }
//         private byte[] IV { get; set; }
//         private bool UseCryptoBody { get; set; }    
//         public AesCryptoHelper(string key, string iv, bool useCrypto)
//         {
//             Key = Encoding.UTF8.GetBytes(key);
//             IV = Encoding.UTF8.GetBytes(iv);
//             UseCryptoBody = useCrypto;
//         }

//         public string Criptografar(string valor)
//         {
//             if (!UseCryptoBody)
//                 return valor;
//             try
//             {
//                 byte[] encrypted = { };
//                 using (var aesAlg = GetAesProvider())
//                 {
//                     var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
//                     using (MemoryStream msEncrypt = new MemoryStream())
//                     {
//                         using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
//                         {
//                             using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
//                             {
//                                 swEncrypt.Write(valor);
//                             }

//                             encrypted = msEncrypt.ToArray();
//                         }
//                     }
//                 }
//                 return Convert.ToBase64String(encrypted);
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//         }
//         public string Descriptografar(string valor)
//         {
//             if (!UseCryptoBody)
//                 return valor;
//             try
//             {
//                 var plaintext = string.Empty;
//                 using (var aesAlg = GetAesProvider())
//                 {
//                     var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
//                     using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(valor)))
//                         using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
//                             using (StreamReader srDecrypt = new StreamReader(csDecrypt))
//                                 plaintext = srDecrypt.ReadToEnd();
//                 }
//                 return plaintext;
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//         }
//         private AesCryptoServiceProvider GetAesProvider()
//         {
//             return new AesCryptoServiceProvider()
//             {
//                 Key = Key,
//                 IV = IV,
//                 Padding = PaddingMode.PKCS7
//             };
//         }
//     }
// }
