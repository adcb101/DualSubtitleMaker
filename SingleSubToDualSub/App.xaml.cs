using Microsoft.Extensions.DependencyInjection;
using SingleSubToDualSub.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SingleSubToDualSub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        //public new static App Current => (App)Application.Current;
        //private IServiceProvider _serviceProvider;
        //public App()
        //{
        //    var serviceCollection = new ServiceCollection();
        //    //serviceCollection.AddLogging(new NLogAspNetCoreOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
        //    serviceCollection.AddSingleton<IGptHttpClient, GptHttpClient>();
        //    _serviceProvider = serviceCollection.BuildServiceProvider();


    //}
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        bool createdNew;
        Mutex mutex = new Mutex(true, "SingleSubToDual", out createdNew);
        if (!createdNew)
        {
            // 已有一个实例正在运行
            MessageBox.Show("已有一个实例正在运行，禁止多个启动！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            mutex.Dispose();
            Shutdown();
            return;
        }
        //IGptHttpClient gptHttpClient;
        // 如果没有其他实例运行，则正常启动应用程序   
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }

}
}

