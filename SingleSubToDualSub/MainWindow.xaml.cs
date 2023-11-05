using Microsoft.Extensions.Logging;
using OpenAI.ObjectModels.ResponseModels;
using SingleSubToDualSub.Models;
using SingleSubToDualSub.Services;
using SingleSubToDualSub.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace SingleSubToDualSub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //_httpClient= httpClient;
            enPathTb.Text = @"F:\PredictingAlpha\sub\2\1";
            cnPathTb.Text = @"F:\PredictingAlpha\sub\2\1";
            merPathTb.Text = @"F:\PredictingAlpha\2\1";
            saveFileNameTb.Text = @"";
        }
        private void combineBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string enPath = enPathTb.Text;
                string cnPath = cnPathTb.Text;
                //string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
                if (!Directory.Exists(enPath))
                {
                    MessageBox.Show("不存在字幕文件", "异常", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // 获取指定文件夹中的所有文件
                string[] fileNames = Directory.GetFiles(enPathTb.Text);
                using (StreamWriter writer = new StreamWriter("F:\\PredictingAlpha\\question.txt", false, Encoding.UTF8))
                {
                    // 遍历所有文件名
                    foreach (var fileName in fileNames)
                    {

                        string gptSRT = System.IO.Path.GetFileName(fileName);
                        if (gptSRT.Contains("_zh_gpt"))
                        {
                            string enSrt = gptSRT.Replace("_zh_gpt", "");
                            // 合并后的字幕文件路径
                            string mergedSubtitlePath = Path.Combine(merPathTb.Text, enSrt);
                            // 读取第一个字幕文件的内容
                            string enContent = File.ReadAllText(Path.Combine(enPath, enSrt), Encoding.UTF8);
                            // 读取第二个字幕文件的内容
                            string cnContent = File.ReadAllText(Path.Combine(enPath, gptSRT), Encoding.UTF8);
                            // 合并两个字幕文件的内容
                            // 合并两个字幕文件的内容
                            //string mergedSubtitleContent = cnContent + "\n" + enContent + "\n";
                            //List<SubtitleBlock> mergedSubt = SubtitleBlock.ParseSubtitles(mergedSubtitleContent).OrderBy(x => x.Index).ToList();
                            List<SubtitleBlock> cnSubt = SubtitleBlock.ParseSubtitles(cnContent).OrderBy(x => x.Index).ToList();
                            List<SubtitleBlock> enSubt = SubtitleBlock.ParseSubtitles(enContent).OrderBy(x => x.Index).ToList();
                            if ((enSubt.Count - cnSubt.Count)>1)
                            {
                                // Path.Combine(enPath, gptSRT);



                                writer.WriteLine(Path.Combine(enPath, gptSRT) + "  " + $"cn:{enSubt.Count}" + $"en:{cnSubt.Count}");
                                writer.WriteLine("");


                            }

                            //using (StreamWriter writer = new StreamWriter(mergedSubtitlePath, false, Encoding.UTF8))
                            //{
                            //    int index = 1;
                            //    foreach (var item in mergedSubt)
                            //    {
                            //        writer.WriteLine(index++);
                            //        //writer.WriteLine("\n");
                            //        writer.WriteLine(item.From + " --> " + item.To);
                            //        //writer.WriteLine("\n");
                            //        writer.WriteLine(item.Text);
                            //        writer.WriteLine("");
                            //    }
                            //}
                        }

                    }
                }

                MessageBox.Show("合并完成！", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }



        //private async void combineBtn_Click(object sender, RoutedEventArgs e)
        //{


        //        try
        //        {
        //            string enPath = enPathTb.Text;
        //            string cnPath = cnPathTb.Text;
        //            //string rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
        //            if (!File.Exists(enPath))
        //            {
        //                MessageBox.Show("不存在字幕文件", "异常", MessageBoxButton.OK, MessageBoxImage.Error);
        //                return;
        //            }
        //            if (string.IsNullOrEmpty(saveFileNameTb.Text))
        //            {
        //                MessageBox.Show("请输入文件名", "异常", MessageBoxButton.OK, MessageBoxImage.Error);

        //                return;
        //            }
        //            string mergedSubtitlePath = merPathTb.Text + saveFileNameTb.Text + "_dualsub.srt"; // 合并后的字幕文件路径
        //                                                                                               // 读取第一个字幕文件的内容
        //            string enContent = File.ReadAllText(enPath, Encoding.UTF8);
        //            // 读取第二个字幕文件的内容
        //            //string cnContent = File.ReadAllText(cnPath, Encoding.UTF8);
        //            // 合并两个字幕文件的内容

        //            //string[] enSub = enContent.Split(new string[] { "\n\n" };
        //            List<SubtitleBlock> cnSubt = SubtitleBlock.ParseSubtitles(enContent);

        //            int batchSize = 20;
        //            var modifiedcnSubt = new List<SubtitleBlock>();
        //            var batches =  cnSubt.Select((item, index) => new { item, index })
        //                .GroupBy(x => x.index / batchSize)
        //                .Select(async group =>
        //                {

        //                    string joinedString = string.Join("\"\"\"\n\"\"\"", group.Select(p =>p.index+1 +"."+p.item.Text));
        //                    //joinedString = "\"\"\"" + joinedString + "\"\"\"";
        //                    var translatedSub =await  GptTranslate($"\"\"\"{joinedString}\"\"\"");
        //                    //await Task.Delay(15000);
        //                    string[] nameArray = translatedSub.Trim('\"').Split("\"\"\"\n\"\"\"");
        //                    int translatedSubCount = nameArray.Length;

        //                    if (translatedSubCount!=batchSize)
        //                    {
        //                        MessageBox.Show("数量不一致！", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //                        var modifiedcnSubtnull = new List<SubtitleBlock>();
        //                        return modifiedcnSubtnull;
        //                    }
        //                    foreach (var entity in group)
        //                    {

        //                        int dotIndex = nameArray[entity.index].IndexOf('.');
        //                        entity.item.Text = nameArray[entity.index].Substring(dotIndex + 2);
        //                    }
        //                    return group.Select(x => x.item).ToList();
        //                });

        //        if (batches==null||batches.Count()==0)
        //        {
        //            MessageBox.Show("数量不一致！", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //            return;
        //        }
        //            foreach (var batch in batches)
        //            {
        //                modifiedcnSubt.AddRange(await (batch).ConfigureAwait(false));
        //            }
        //            //for (int i = 0; i < cnSubt.Count; i++)
        //            //{
        //            //    cnSubt[i] = modifiedEntites[i];
        //            //}

        //            //await cnSubt.CustomModify(async item => await GptTranslate(item));

        //            string cnSubtitlePath = merPathTb.Text + saveFileNameTb.Text + "_zh_gpt.srt"; // 中文字幕文件路径

        //            using (StreamWriter writer = new StreamWriter(cnSubtitlePath, false, Encoding.UTF8))
        //            {
        //                //int index = 1;
        //                foreach (var item in modifiedcnSubt)
        //                {
        //                    writer.WriteLine(item.Index);
        //                    //writer.WriteLine("\n");
        //                    writer.WriteLine(item.From + " --> " + item.To);
        //                    //writer.WriteLine("\n");
        //                    writer.WriteLine(item.Text);
        //                    writer.WriteLine("");
        //                }
        //            }

        //            string cnContent = File.ReadAllText(cnSubtitlePath, Encoding.UTF8);
        //            // 合并两个字幕文件的内容
        //            string mergedSubtitleContent = cnContent + "\n" + enContent + "\n";
        //            List<SubtitleBlock> mergedSubt = SubtitleBlock.ParseSubtitles(enContent).OrderBy(x => x.Index).ToList();
        //            using (StreamWriter writer = new StreamWriter(mergedSubtitlePath, false, Encoding.UTF8))
        //            {
        //                int index = 1;
        //                foreach (var item in mergedSubt)
        //                {
        //                    writer.WriteLine(index++);
        //                    //writer.WriteLine("\n");
        //                    writer.WriteLine(item.From + " --> " + item.To);
        //                    //writer.WriteLine("\n");
        //                    writer.WriteLine(item.Text);
        //                    writer.WriteLine("");
        //                }
        //            }
        //            await Task.Delay(10);
        //            //File.Delete(enPath);
        //            //File.Delete(cnPath);
        //            MessageBox.Show("合并完成！", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "异常", MessageBoxButton.OK, MessageBoxImage.Error);

        //        }
        //        //item => ToUpperCustom(item)







        //}
        //private readonly ILogger<GptHttpClient> _logger;
        private GptHttpClient _httpClient = new GptHttpClient();
        private async Task<string> GptTranslate(string subBatches)
        {


            RequestData requestData = new RequestData();
            Requestmessage sysmessage = new Requestmessage();
            sysmessage.role = "system";
            sysmessage.content = $@"You are an options finance expert and are proficient in multiple languages, especially Chinese and English. Please accurately translate the three quotation marks I provided into Chinese. Every translation of triple-delimited content must be triple-delimited. In order to strictly adhere to this rule, I will enter the content of a fixed 20 three quotation marks, and you must also output the translation content of 20 three quotation marks. In order to achieve this, you do not have to maintain the continuity of the translation of the upper and lower sentences.";
            Requestmessage usermessage = new Requestmessage();
            usermessage.role = "user";
            usermessage.content = sysmessage.content + "\n" + subBatches;

            //requestData.messages.Add(sysmessage);
            requestData.messages.Add(usermessage);

            var response = await _httpClient.GetCompletion(requestData, "bear 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");
            var result = await response.Content.ReadFromJsonAsync<ChatCompletion>();


            return result.choices[0].message.Content;

            //return Json(result.choices[0].message.Content);
            //throw new NotImplementedException();
        }


    }

    public static class ListExtensions
    {
        public static async Task<List<T>> CustomModify<T>(this List<T> list, Func<T, Task<T>> modifier)
        {    // 使用比较器对列表进行排序
            //list.Sort((x, y) => comparer(x, y));
            for (int i = 0; i < list.Count; i++)
            {

                list[i] = await modifier(list[i]);

            }
            return list;
        }
    }
}
