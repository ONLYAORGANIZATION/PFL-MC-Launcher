﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using KMCCC.Launcher;
using KMCCC.Authentication;
using SquareMinecraftLauncher;

namespace PFL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SquareMinecraftLauncher.Minecraft.Tools tools = new SquareMinecraftLauncher.Minecraft.Tools();
        public void GameStart()//定义启动游戏函数
        {
            if (versionCombo.Text != string.Empty && javaCombo.Text != string.Empty && IdTextbox.Text != string.Empty && MemoryTextbox.Text != string.Empty)
            {
                try
                {
                    Core.JavaPath = javaCombo.SelectedValue.ToString();
                    var ver = (KMCCC.Launcher.Version)versionCombo.SelectedItem;
                    var result = Core.Launch(new LaunchOptions
                    {
                        Version = ver, //Ver为Versions里你要启动的版本名字
                        MaxMemory = Convert.ToInt32(MemoryTextbox.Text), //最大内存，int类型
                        Authenticator = new OfflineAuthenticator(IdTextbox.Text), //离线启动
                        Mode = LaunchMode.MCLauncher, //启动模式
                    });
                    if (!result.Success)
                    {
                        switch (result.ErrorType)
                        {
                            case ErrorType.NoJAVA:
                                MessageBox.Show("java错误，详情信息：" + result.ErrorMessage, "错误");
                                break;
                            case ErrorType.AuthenticationFailed:
                                MessageBox.Show("登录错误，详情信息：" + result.ErrorMessage, "错误");
                                break;
                            case ErrorType.UncompressingFailed:
                                MessageBox.Show("文件错误，详情信息：" + result.ErrorMessage, "错误");
                                break;
                            default:
                                MessageBox.Show(result.ErrorMessage, "错误");
                                break;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("启动失败", "错误");//里面是报错信息和标题
                }
            }
        }
        public static LauncherCore Core = LauncherCore.Create();
        public MainWindow()
        {
            InitializeComponent();
            var versions = Core.GetVersions().ToArray();
            versionCombo.ItemsSource = versions;//绑定数据源
            javaCombo.ItemsSource = tools.GetJavaPath();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameStart();//调用启动函数
        }
    }
}
