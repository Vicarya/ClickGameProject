using Microsoft.Win32.SafeHandles;
using System.IO;

namespace myObject
{
    public class LoadCSV
    {
        /// <summary> 現在操作しているファイル </summary>
        static FileStream stream;

        /// <summary>
        /// csvファイルを開き文字列を読込む
        /// </summary>
        /// <param name="directry">ファイルディレクトリ(slnファイルから見たファイルディレクトリ)</param>
        /// <param name="index">何行目を読込むか</param>
        /// <returns></returns>
        public static string[] LoadCsv(string directry, int index)
        {
            stream = new FileStream(directry, FileMode.Open);
            var reader = new StreamReader(stream);
            string tag = reader.ReadLine(); // タグ列の読み流し
            while (true)
            {
                //一行ずつ読み込み、カウントを一つづつ減らす
                //対応列に到達するとindexが0になるので
                //','でパース分けして値を返す
                string[] strs = reader.ReadLine().Split(',');
                if (index == 0)
                {
                    stream.Close();
                    return strs;
                }
                --index;
            }
        }
        public static string[] LoadCsv(UnityEngine.TextAsset textAsset, int index)
        {
            var reader = new StringReader(textAsset.text);
            string tag = reader.ReadLine(); // タグ列の読み流し
            while (true)
            {
                //一行ずつ読み込み、カウントを一つづつ減らす
                //対応列に到達するとindexが0になるので
                //','でパース分けして値を返す
                string[] strs = reader.ReadLine().Split(',');
                if (index == 0)
                {
                    return strs;
                }
                --index;
            }
        }
    }
}