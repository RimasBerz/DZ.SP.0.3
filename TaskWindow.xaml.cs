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
using System.Windows.Shapes;
using System.Threading;
namespace SystemProgramming1
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow()
        {
            InitializeComponent();
        }
#region 1
        private async void ButtonStart1_Click(object sender, RoutedEventArgs e)
        {
            sum = 100;
            ConsoleBlock.Text = "";
            for(int i = 0; i < 10; i++)
            {
                //Task.Run( PlusPercent).Wait();
                await PlusPercent();
            }
        }
    
        private void ButtonStop1_Click(object sender, RoutedEventArgs e)
        {

        }
        private double sum;
        private async Task PlusPercent()
        {
            await Task.Delay(300);
            sum *= 1.1;
           Dispatcher.Invoke(() => ConsoleBlock.Text += $"{sum}\n");
        }
        #endregion
        private void ButtonStart2_Click(object sender, RoutedEventArgs e)
        {
            Task task1 = new Task(proc1);
            //task1.RunSynchronously();
            task1.Start();
          //  Task.Run(proc1);
        }
        private void proc1()
        {
            ConsoleWrite("started\n");
            Thread.Sleep(200);
            ConsoleWrite("finished\n");
        }
        private void ConsoleWrite(Object item)
        {
            this.Dispatcher.Invoke(
                () => ConsoleBlock.Text += item is null ? "NULL" : item.ToString());
        }
        private void ButtonStop2_Click(object sender, RoutedEventArgs e)
        {
            Task task1 = new Task(procN, 1);
            Task task2 = new Task(procN, 2);
            //task1.Start();
            //task2.Start();
            task1.RunSynchronously();
            task2.Start();
        }
        private void procN(object? item)
        {
            ConsoleWrite($"proc{item?.ToString()} started\n");
            Thread.Sleep(200);
            ConsoleWrite($"proc{item?.ToString()} started\n");
        }
        private void ButtonStart3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonStop3_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ButtonDemo3_Click(object sender, RoutedEventArgs e)
        {
            Task task1 = new Task(procN, 1);
            Task task2 = new Task(procN, 2);
            task1.Start();
            //task1.Wait();
            await task1;
            task2.Start();
        }

        private void ButtonDemoNext3_Click(object sender, RoutedEventArgs e)
        {
            Task task1 = new Task(procN, 1);
            Task task2 = new Task(procN, 2);
            task1.ContinueWith(_ => task2.Start())
                .ContinueWith(_ => new Task(procN, 3).Start());
            task1.Start();
            
           
        }

        private void ButtonStart4_Click(object sender, RoutedEventArgs e)
        {
            ConsoleWrite("funcN(1) started\n");
            var task1 = funcN(1);
            ConsoleWrite(task1.Result);
        }

        private void ButtonStop4_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ButtonDemoNext4_Click(object sender, RoutedEventArgs e)
        {
            ConsoleWrite("funcN(2) started\n");
            ConsoleWrite(await funcN(2));
        }
        private async Task<String> funcN(int N)
        {
            await Task.Delay(1000);
            return $"func {N} result\n";
        }

        private async void ButtonDemo4_Click(object sender, RoutedEventArgs e)
        {
            ConsoleWrite("funcN(1) started\n");
            ConsoleWrite(await funcN(1));
            ConsoleWrite("funcN(2) started\n");
            ConsoleWrite(await funcN(2));
        }

        private async void ButtonDemoNextNext4_Click(object sender, RoutedEventArgs e)
        {
            Task<String> task1 = funcN(1);
            Task<String> task2 = funcN(2);
            ConsoleWrite("funcN(1) started\n");
            ConsoleWrite("funcN(2) started\n");
            //String res1 = task1.Result;
            ConsoleWrite(await task1);
            ConsoleWrite(await task2);
        }

        private async void ButtonDemoNextNext4_1_Click(object sender, RoutedEventArgs e)
        {
            Task<String>[] tasks = new Task<String>[7];
            for (int i = 0; i < tasks.Length; i ++)
            {
                tasks[i] = funcN(i);
            }
            //Task.WaitAll(tasks);
            //Task.WaitAny(tasks);
            foreach(var task in tasks)
            {
                ConsoleWrite(await task);
            }
        }
    }
}
