﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    public class SystemsManager
    {
        private MyLogger logger = new MyLogger("SystemsManager");
        private static SystemsManager instance = new SystemsManager();
        public static SystemsManager Instance => instance;

        // Notify PLC
        public NotifyPLCBits NotifyPLCBits;
        public NotifyPLCWord NotifyPLCWord;
        public NotifyPLCDWord NotifyPLCDWord;

        public NotifyPLCWord_ZR NotifyPLCWord_ZR;
        public NotifyPLCDWord_ZR NotifyPLCDWord_ZR;

        public NotifyEvenDataVision NotifyEvenDataVision;

        public bool isConnectPLC = false;
        public bool DeviceLoadBit_M = false;
        public bool DeviceLoadBit_X = false;
        public bool DeviceLoadBit_Y = false;
        public bool DeviceLoadBit_L = true;
        public bool DeviceLoadBit_K = true;
        public bool DeviceLoadWord = false;
        public bool DeviceLoadDWord = false;
        public bool DeviceLoadWord_ZR = false;
        public bool DeviceLoadDWord_ZR = false;

        public bool isWriteDevice = false;


        public NotifyEvenMES NotifyEvenMES;
        public NotifyEvenTester NotifyEvenTester;
        public void StartUp()
        {
            this.LoadNotifyEven();

            logger.Create("SystemsManager Program Start Up", LogLevel.Error);
        }
        private void LoadNotifyEven()
        {
            this.LoadNotifyPLCBits();
            this.LoadNotifyPLCWord();
            this.LoadNotifyPLCDWord();
            this.LoadNotifyPLCDWord_ZR();
            this.LoadNotifyPLCWord_ZR();
            this.LoadNotìyTester();

            LoadNotifyEvenDataVision();
        }
        private void LoadNotìyTester()
        {
            NotifyEvenTester = new NotifyEvenTester();
        }
        private void LoadNotifyPLCBits()
        {
            this.NotifyPLCBits = new NotifyPLCBits();
        }
        private void LoadNotifyPLCWord()
        {
            this.NotifyPLCWord = new NotifyPLCWord();
        }
        private void LoadNotifyPLCDWord()
        {
            this.NotifyPLCDWord = new NotifyPLCDWord();
        }
        private void LoadNotifyPLCDWord_ZR()
        {
            this.NotifyPLCDWord_ZR = new NotifyPLCDWord_ZR();
        }
        private void LoadNotifyPLCWord_ZR()
        {
            this.NotifyPLCWord_ZR = new NotifyPLCWord_ZR();
        }

        private void LoadNotifyEvenDataVision()
        {
            this.NotifyEvenDataVision = new NotifyEvenDataVision();
        }
        // Load Even MES
        private void LoadNotifyEvenMES()
        {
            this.NotifyEvenMES = new NotifyEvenMES();
        }
    }
}
