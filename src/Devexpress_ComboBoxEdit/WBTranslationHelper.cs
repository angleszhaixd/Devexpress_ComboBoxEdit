using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft;

namespace Devexpress_ComboBoxEdit
{
    /// <summary>
    /// 汉子转五笔工具类 
    /// author:zhaixd
    /// date:2017.08.02
    /// </summary>
    public class WBTranslationHelper
    {
        /// <summary>
        /// 五笔数据字典文件路径，必须为json文件，不设置默认从bin目录下读取(/bin/wb.json)
        /// </summary>
        private static string WBDataFilePath;
        /// <summary>
        /// 五笔字典编码表
        /// </summary>
        public static List<WBItem> WBDictionarys { get { return LazyWBDictionarys.Value; } }

        private readonly static Lazy<List<WBItem>> LazyWBDictionarys;

        static WBTranslationHelper()
        {
            LazyWBDictionarys = new Lazy<List<WBItem>>(() =>
            {
                List<WBItem> _wbDictionarys = new List<WBItem>();
                #region 读取五笔数据字典
                using (var fileStream = File.OpenRead(WBDataFilePath))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        var wbJsonData = reader.ReadToEnd();
                        _wbDictionarys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WBItem>>(wbJsonData);
                    }
                }
                #endregion
                return _wbDictionarys;
            });
        }

        /// <summary>
        /// 校验五笔数据库字典文件是否存在
        /// </summary>
        /// <returns></returns>
        private static string checkFileName()
        {
            string message = string.Empty;
            if (string.IsNullOrWhiteSpace(WBDataFilePath) || !File.Exists(WBDataFilePath))
            {
                message = "五笔编码数据库字典路径未设置，请将字典库文件(.json)拷贝至应用程序根目录。字典文件的内容格式:[{\"WBBM\": \"yvny\",\"HZ\": \"良心\"}]";
                throw new FileNotFoundException(message);
            }
            return message;
        }

        /// <summary>
        /// 初始化设置五笔数据字典库路径 自动设置五笔字典路径为/bin/wb.json
        /// </summary>
        public static void Init()
        {
            var defaultFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wb.json");
            Init(defaultFileName);
        }

        /// <summary>
        /// 初始化设置五笔数据字典库路径
        /// </summary>
        /// <param name="filePath">五笔数据字典路径，必须为json文件，格式：[{"WBBM": "yvny","HZ": "良心"}]</param>
        public static void Init(string filePath)
        {
            WBDataFilePath = filePath;
            checkFileName();
        }
        /// <summary>
        /// 获取输入文字的五笔码全码（调用此方法前必须调用Init方法设置五笔数据字典库文件路径）
        /// </summary>
        /// <param name="str">输入的文本</param>
        /// <returns></returns>
        public static string GetWbAll(string input)
        {
            //校验当前是否设置五笔字典库路径
            var message = checkFileName();
            if (!string.IsNullOrWhiteSpace(message))
            {
                throw new FileNotFoundException(message);
            }
            if (string.IsNullOrWhiteSpace(input))
                return null;
            //去除input中的空格
            input = input.Replace(" ", "");
            StringBuilder result = new StringBuilder();  //返回字符串
            try
            {
                //1、整个词组完全匹配
                var item =  WBDictionarys.FirstOrDefault(x => x.HZ == input);
                if (item != null)
                {
                    result.Append(item.WBBM.ToUpper());
                }
                else
                { 
                    //2、循环获取每个字符的五笔编码
                    List<string> inputArr = new List<string>();
                    for (int i = 0; i < input.Length; i++)
                    {
                        string temp = input.Substring(i, 1);
                        inputArr.Add(temp);
                    }
                    inputArr.ForEach(str =>
                    {
                        var filterCodes = WBDictionarys.Where(x => x.HZ == str).OrderBy(o=>o.WBBM).ToList();
                        if (filterCodes.Count > 0)
                        {
                            result.Append(filterCodes.FirstOrDefault().WBBM.ToUpper());
                        }
                    });
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result.ToString(); 
        }

        /// <summary>
        /// 获取输入文字的五笔码的第一笔（调用此方法前必须调用Init方法设置五笔数据字典库文件路径）
        /// </summary>
        /// <param name="str">输入的文本</param>
        /// <returns></returns>
        public static string GetWbFirst(string input)
        {
            //校验当前是否设置五笔字典库路径
            var message = checkFileName();
            if (!string.IsNullOrWhiteSpace(message))
            {
                throw new FileNotFoundException(message);
            }
            if (string.IsNullOrWhiteSpace(input))
                return null;
            //去除input中的空格
            input = input.Replace(" ", "");
            StringBuilder result = new StringBuilder();  //返回字符串
            try
            {
                //循环获取每个字符的五笔编码的第一个字符
                List<string> inputArr = new List<string>();
                for (int i = 0; i < input.Length; i++)
                {
                    string temp = input.Substring(i, 1);
                    inputArr.Add(temp);
                }
                inputArr.ForEach(str =>
                {
                    var filterCodes = WBDictionarys.Where(x => x.HZ == str).OrderBy(o => o.WBBM).ToList();
                    if (filterCodes.Count > 0)
                    {
                        result.Append(filterCodes.FirstOrDefault().WBBM.Substring(0, 1).ToUpper());
                    }
                });
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result.ToString();
        }

        /// <summary>
        /// 五笔数据字典项
        /// </summary>
        public class WBItem
        {
            /// <summary>
            /// 汉子
            /// </summary>
            public string HZ { get; set; }

            /// <summary>
            /// 五笔字符
            /// </summary>
            public string WBBM { get; set; }
        }
    }
}
