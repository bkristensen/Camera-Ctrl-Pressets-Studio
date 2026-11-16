using System;
using System.Security.Cryptography;
namespace BrioPresets {
 public static class SettingsCode {
  public static bool IsSet(SavedData data){return !string.IsNullOrEmpty(data.SettingsCodeHash);}
  public static void Set(SavedData data,string code) {
   if(string.IsNullOrEmpty(code)){data.SettingsCodeHash=null;data.SettingsCodeSalt=null;return;}
   if(code.Length<4||code.Length>64)throw new ArgumentException(UiText.T("Use a code of 4–64 characters.","Brug en kode på 4–64 tegn."));
   var salt=new byte[16];using(var rng=RandomNumberGenerator.Create())rng.GetBytes(salt);
   data.SettingsCodeSalt=Convert.ToBase64String(salt);data.SettingsCodeHash=Convert.ToBase64String(Hash(code,salt));
  }
  static byte[] Hash(string code,byte[] salt){using(var kdf=new Rfc2898DeriveBytes(code,salt,100000,HashAlgorithmName.SHA256))return kdf.GetBytes(32);}
  public static bool Verify(SavedData data,string code) {
   if(!IsSet(data))return true;
   try{var expected=Convert.FromBase64String(data.SettingsCodeHash);var actual=Hash(code??"",Convert.FromBase64String(data.SettingsCodeSalt));int diff=actual.Length^expected.Length;
    for(int i=0;i<Math.Min(actual.Length,expected.Length);i++)diff|=actual[i]^expected[i];return diff==0;
   }catch(FormatException){return false;}catch(ArgumentNullException){return false;}
  }
 }
}
