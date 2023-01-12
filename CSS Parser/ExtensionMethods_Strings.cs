using System.IO;
using System;
using System.Text.RegularExpressions;


namespace JoCssParser {

/// <summary>
/// String Extension Methods for less annoying .Net String handling
/// (c) Thomas Schittli 
/// </summary>
public static class ExtensionMethodsStrings {

   public static bool ØHasValue(this string str) => !string.IsNullOrWhiteSpace(str);


   /// <summary>
   /// Extension-Method for Strings
   /// 
   /// You can call "".ØSubstring() and you always get what you expect
   /// and you never get an exception as M$ .Net is doing if a parameter is 'invalid' :-(
   ///  
   /// </summary>
   /// <param name="str"></param>
   /// <param name="start"></param>
   /// <param name="length"></param>
   /// <returns></returns>
   public static string ØSubstring(this string str, int start, int length) {
      if (ØHasValue(str)) {
         return str.Substring(Math.Min(str.Length, start), Math.Min(str.Length, length));
      }

      return "";
   }

   /// <summary>
   /// Löscht leere Zeilen
   /// </summary>
   /// <param name="str"></param>
   /// <returns></returns>
   public static string ØRemoveEmptyLines(this string str) {
      if (ØHasValue(str)) {
         return Regex.Replace
            (str
             , @"^([\t ]*\r?\n)*"
             , string.Empty
             , RegexOptions.Multiline);
      }
      return "";
   }


   /// <summary>
   /// float in B, KB, MB, ... formatieren
   /// </summary>
   /// <param name="bytes"></param>
   /// <param name="Nachkommastellen"></param>
   /// <param name="Delimiter"></param>
   /// <returns></returns>
   public static string ØToUnitStr(this float bytes, int Nachkommastellen = 3, string Delimiter = " ") {
      string[] suffix = { "B", "KB", "MB", "GB", "TB" };
      int      i;
      double   doubleBytes = 0;

      for (i = 0; (int)(bytes/1024) > 0; i++, bytes /= 1024) {
         doubleBytes = bytes/1024.0;
      }

      switch (Nachkommastellen) {
         case 0:
            return string.Format("{0:0}{1}{2}", doubleBytes, Delimiter, suffix[i]);

         case 1:
            return string.Format("{0:0.0}{1}{2}", doubleBytes, Delimiter, suffix[i]);

         case 2:
            return string.Format("{0:0.00}{1}{2}", doubleBytes, Delimiter, suffix[i]);

         default:
         case 3:
            return string.Format("{0:0.000}{1}{2}", doubleBytes, Delimiter, suffix[i]);
      }
   }


   /// <summary>
   /// str.Trim()
   /// und dann alle mehrfachen Leerzeichen auf jeweils eines reduzieren
   /// </summary>
   /// <param name="str"></param>
   /// <returns></returns>
   public static string ØTrimAndReduce(this string str) => Regex.Replace(str, @"\s+", " ");


   /// <summary>
   /// Zähle die Anz. Worte, die getrennt sind durch
   /// ' ', '.', '?'
   /// </summary>
   /// <param name="str"></param>
   /// <returns></returns>
   public static int ØWordCount(this string str) => str.Split(
                                                              new[] { ' ', '.', '?' }
                                                              , StringSplitOptions.RemoveEmptyEntries)
                                                       .Length;


   /// <summary>
   /// Erzeugt aus einem String einen MemoryStream
   ///
   /// !Q https://stackoverflow.com/a/1879470/4795779
   /// !i Alternative mit Codireung
   ///   https://stackoverflow.com/a/5238289/4795779
   /// </summary>
   /// <param name="str"></param>
   /// <returns></returns>
   public static MemoryStream ØToStream(this string str) {
      var stream = new MemoryStream();
      var writer = new StreamWriter(stream);
      writer.Write(str);
      writer.Flush();
      stream.Position = 0;

      return stream;
   }

}


}

