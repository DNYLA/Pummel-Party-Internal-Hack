using SharpMonoInjector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PummelPartyInjector
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //const string AssemblyPath = @"C:\Users\Danny\\source\repos\PummelPartyHack\PummelPartyHack2\bin\Debug";
        const string AssemblyPath = "PummelPartyHack.dll";
        const string InjectNamespace = "PummelPartyHack";
        const string InjectClassName = "Loader";
        const string InjectMethodName = "Init";
        const string EjectMethodName = "Unload";
        MonoProcess SelectedProcess = new MonoProcess();
        private InjectedAssembly injectedAssembly = new InjectedAssembly();
        private bool AutoStart = false;
        private bool GetProcess()
        {
            //foreach (Process p in Process.GetProcessesByName("PummelParty"))
            foreach (Process p in Process.GetProcesses())
            {
                if (!p.ProcessName.Contains("PummelParty")) continue;

                const ProcessAccessRights flags = ProcessAccessRights.PROCESS_QUERY_INFORMATION | ProcessAccessRights.PROCESS_VM_READ;

                IntPtr handle;
                handle = Native.OpenProcess(flags, false, p.Id);

                if (ProcessUtils.GetMonoModule(handle, out IntPtr mono))
                {
                    if (p.ProcessName.Contains("PummelParty"))
                    {
                        SelectedProcess.MonoModule = mono;
                        SelectedProcess.Id = p.Id;
                        SelectedProcess.Name = p.ProcessName;
                    }
                    return true;
                }

            }

            string name = SelectedProcess.Name;
            Console.WriteLine(name);
            if (name == null)
            {
                if (AutoStart)
                {
                    Process.Start(@"D:\Games\Pummel Party\steamapps\common\Pummel Party\PummelParty.exe - Shortcut");
                    Thread.Sleep(6000);
                    return GetProcess();
                }

                MessageBox.Show("PummelParty Process Not found");
                return false;
            }

            return false;
        }

        private void InjectCheat()
        {
            bool gotProcess = GetProcess();
            if (!gotProcess) return;

            IntPtr handle = Native.OpenProcess(ProcessAccessRights.PROCESS_ALL_ACCESS, false, SelectedProcess.Id);

            if (handle == IntPtr.Zero)
            {
                //Status = "Failed to open process";
                return;
            }

            byte[] file;

            try
            {
                file = File.ReadAllBytes(AssemblyPath);
            }
            catch (IOException)
            {
                //Status = "Failed to read the file " + AssemblyPath;
                return;
            }

            //IsExecuting = true;
            //Status = "Injecting " + Path.GetFileName(AssemblyPath);


            using (Injector injector = new Injector(handle, SelectedProcess.MonoModule))
            {
                try
                {
                    IntPtr asm = injector.Inject(file, InjectNamespace, InjectClassName, InjectMethodName);

                    injectedAssembly.ProcessId = SelectedProcess.Id;
                    injectedAssembly.Address = asm;
                    injectedAssembly.Name = Path.GetFileName(AssemblyPath);
                    injectedAssembly.Is64Bit = injector.Is64Bit;
                    //Status = "Injection successful";
                }
                catch (InjectorException ie)
                {
                    //Status = "Injection failed: " + ie.Message;
                }
                catch (Exception ex)
                {
                    //Status = "Injection failed (unknown error): " + e.Message;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InjectCheat();
        }



        private void EjectAssembly()
        {
            IntPtr handle = Native.OpenProcess(ProcessAccessRights.PROCESS_ALL_ACCESS, false, injectedAssembly.ProcessId);

            if (handle == IntPtr.Zero)
            {
                //Status = "Failed to open process";
                return;
            }

            //IsExecuting = true;
            //Status = "Ejecting " + SelectedAssembly.Name;

            ProcessUtils.GetMonoModule(handle, out IntPtr mono);

            using (Injector injector = new Injector(handle, mono))
            {
                try
                {
                    injector.Eject(injectedAssembly.Address, InjectNamespace, InjectClassName, EjectMethodName);
                    //Status = "Ejection successful";
                }
                catch (InjectorException ie)
                {
                    //Status = "Ejection failed: " + ie.Message;
                }
                catch (Exception e)
                {
                    //Status = "Ejection failed (unknown error): " + e.Message;
                }
            }

            //IsExecuting = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            EjectAssembly();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (AutoStart)
                InjectCheat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EjectAssembly();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EjectAssembly();
            Process.GetProcessById(SelectedProcess.Id).Kill();
        }
    }
    public class MonoProcess
    {
        public IntPtr MonoModule { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }


    public class InjectedAssembly
    {
        public int ProcessId { get; set; }

        public IntPtr Address { get; set; }

        public bool Is64Bit { get; set; }

        public string Name { get; set; }
    }
}
