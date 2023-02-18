using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Virtua_Cop_2_trainer
{
    class MAPI
    {
        // TRY TO FIND A WAY TO GIVE THE RIGHT ACCESS ON PROCESS OPENING (NEW WAY)
        [Flags]
        public enum ProcessAccessType
        {
            PROCESS_TERMINATE = (0x0001),
            PROCESS_CREATE_THREAD = (0x0002),
            PROCESS_SET_SESSIONID = (0x0004),
            PROCESS_VM_OPERATION = (0x0008),
            PROCESS_VM_READ = (0x0010),
            PROCESS_VM_WRITE = (0x0020),
            PROCESS_DUP_HANDLE = (0x0040),
            PROCESS_CREATE_PROCESS = (0x0080),
            PROCESS_SET_QUOTA = (0x0100),
            PROCESS_SET_INFORMATION = (0x0200),
            PROCESS_QUERY_INFORMATION = (0x0400)
        }

        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] bBuffer, UInt32 size, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] bBuffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);

        // This is an old way to open process, may want to avoid this
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
    }

    public class Memory
    {
        public Memory()
        {
        }
        private Process mReadProcess = null;
        private IntPtr hReadProcess = IntPtr.Zero;

        MAPI.ProcessAccessType access = MAPI.ProcessAccessType.PROCESS_VM_READ
      | MAPI.ProcessAccessType.PROCESS_VM_WRITE
      | MAPI.ProcessAccessType.PROCESS_VM_OPERATION
            | MAPI.ProcessAccessType.PROCESS_SET_INFORMATION
            | MAPI.ProcessAccessType.PROCESS_QUERY_INFORMATION;

        //Open Process
        public bool OpenProcess()
        {
            mReadProcess = Process.GetCurrentProcess();
            if (mReadProcess.Handle != IntPtr.Zero)
            {
                hReadProcess = mReadProcess.Handle;
                return true;
            }
            else
                return false;
        }

        public bool GetProcessOldWay(string sProcessName)
        {
            Process[] aProcesses = Process.GetProcessesByName(sProcessName);
            if (aProcesses.Length > 0)
            {
                mReadProcess = aProcesses[0];

                return true;
            }
            return false;
        }

        public void OpenOldWay()
        {
            hReadProcess = MAPI.OpenProcess((uint)access, 1, (uint)mReadProcess.Id);
        }

        public bool OpenProcess(string sProcessName)
        {
            Process[] aProcesses = Process.GetProcessesByName(sProcessName);
            if (aProcesses.Length > 0)
            {
                mReadProcess = aProcesses[0];
                if (mReadProcess.Handle != IntPtr.Zero)
                {
                    hReadProcess = mReadProcess.Handle;
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        public bool OpenProcess(int iProcessID)
        {
            mReadProcess = Process.GetProcessById(iProcessID);
            if (mReadProcess.Handle != IntPtr.Zero)
            {
                hReadProcess = mReadProcess.Handle;
                return true;
            }
            else
                return false;
        }

        //Modules
        private ProcessModule FindModule(string sModuleName)
        {
            for (int iModule = 0; iModule < mReadProcess.Modules.Count; iModule++)
            {
                if (mReadProcess.Modules[iModule].ModuleName == sModuleName)
                    return mReadProcess.Modules[iModule];
            }
            return null;
        }
        public ProcessModuleCollection GetModules()
        {
            return mReadProcess.Modules;
        }

        //Get Process Info
        public string Name()
        {
            return mReadProcess.ProcessName;
        }
        public int PID()
        {
            return mReadProcess.Id;
        }
        public int SID()
        {
            return mReadProcess.SessionId;
        }
        public string FileVersion()
        {
            return mReadProcess.MainModule.FileVersionInfo.FileVersion;
        }
        public string StartTime()
        {
            return mReadProcess.StartTime.ToString();
        }

        //Get Module Info
        public int BaseAddress()
        {
            return mReadProcess.MainModule.BaseAddress.ToInt32();
        }
        public int BaseAddress(string sModuleName)
        {
            return FindModule(sModuleName).BaseAddress.ToInt32();
        }
        public int EntryPoint()
        {
            return mReadProcess.MainModule.EntryPointAddress.ToInt32();
        }
        public int EntryPoint(string sModuleName)
        {
            return FindModule(sModuleName).EntryPointAddress.ToInt32();
        }
        public int MemorySize()
        {
            return mReadProcess.MainModule.ModuleMemorySize;
        }
        public int MemorySize(string sModuleName)
        {
            return FindModule(sModuleName).ModuleMemorySize;
        }

        //Read from Address
        public byte ReadByte(int iMemoryAddress)
        {
            byte[] bBuffer = new byte[1]; IntPtr ptrBytesRead;
            int iReturn = MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 1, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return bBuffer[0];
        }
        public ushort ReadShort(int iMemoryAddress)
        {
            byte[] bBuffer = new byte[2]; IntPtr ptrBytesRead;
            int iReturn = MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 2, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToUInt16(bBuffer, 0);
        }
        public uint ReadInt(int iMemoryAddress)
        {
            byte[] bBuffer = new byte[4]; IntPtr ptrBytesRead;
            int iReturn = MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToUInt32(bBuffer, 0);
        }
        public long ReadLong(int iMemoryAddress)
        {
            byte[] bBuffer = new byte[8]; IntPtr ptrBytesRead;
            int iReturn = MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToInt64(bBuffer, 0);
        }
        public float ReadFloat(int iMemoryAddress)
        {
            byte[] bBuffer = new byte[4]; IntPtr ptrBytesRead;
            int iReturn = MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToSingle(bBuffer, 0);
        }
        public double ReadDouble(int iMemoryAddress)
        {
            byte[] bBuffer = new byte[8]; IntPtr ptrBytesRead;
            int iReturn = MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToDouble(bBuffer, 0);
        }
        public string ReadString(int iMemoryAddress, uint iTextLength, int iMode = 0)
        {
            byte[] bBuffer = new byte[iTextLength]; IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, iTextLength, out ptrBytesRead);

            if (iMode == 0)
                return Encoding.UTF8.GetString(bBuffer);
            else if (iMode == 1)
                return BitConverter.ToString(bBuffer).Replace("-", "");
            else
                return "";
        }
        public byte[] ReadAOB(int iMemoryAddress, uint iBytesToRead)
        {
            byte[] bBuffer = new byte[iBytesToRead]; IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, iBytesToRead, out ptrBytesRead);

            return bBuffer;
        }

        //Write to Address
        public bool Write(int iMemoryAddress, byte bByteToWrite)
        {
            byte[] bBuffer = { bByteToWrite }; IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 1, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 1);
        }
        public bool Write(int iMemoryAddress, short iShortToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iShortToWrite); IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 2, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 2);
        }
        public bool Write(int iMemoryAddress, int iIntToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iIntToWrite); IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 4);
        }
        public bool Write(int iMemoryAddress, long iLongToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iLongToWrite); IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 8);
        }
        public bool Write(int iMemoryAddress, float iFloatToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iFloatToWrite); IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 4);
        }
        public bool Write(int iMemoryAddress, double iDoubleToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iDoubleToWrite); IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 8);
        }
        public bool Write(int iMemoryAddress, string sStringToWrite, int iMode = 0)
        {
            byte[] bBuffer = { 0 }; IntPtr ptrBytesWritten;

            if (iMode == 0)
                bBuffer = CreateAOBText(sStringToWrite);
            else if (iMode == 1)
                bBuffer = ReverseBytes(CreateAOBString(sStringToWrite));

            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, (uint)bBuffer.Length, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == bBuffer.Length);
        }
        public bool Write(int iMemoryAddress, byte[] bBytesToWrite)
        {
            IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBytesToWrite, (uint)bBytesToWrite.Length, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == bBytesToWrite.Length);
        }
        public bool NOP(int iMemoryAddress, int iLength)
        {
            byte[] bBuffer = new byte[iLength]; IntPtr ptrBytesWritten;
            for (int i = 0; i < iLength; i++)
                bBuffer[i] = 0x90;

            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, (uint)iLength, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == iLength);
        }

        //Read from Pointer
        public byte ReadByte(int iMemoryAddress, int[] iOffsets)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[1];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 1, out ptrBytesRead);
            return bBuffer[0];
        }
        public ushort ReadShort(int iMemoryAddress, int[] iOffsets)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[2];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 2, out ptrBytesRead);
            return BitConverter.ToUInt16(bBuffer, 0);
        }
        public uint ReadInt(int iMemoryAddress, int[] iOffsets)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[4];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 4, out ptrBytesRead);
            return BitConverter.ToUInt32(bBuffer, 0);
        }
        public long ReadLong(int iMemoryAddress, int[] iOffsets)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[8];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 8, out ptrBytesRead);
            return BitConverter.ToInt64(bBuffer, 0);
        }
        public float ReadFloat(int iMemoryAddress, int[] iOffsets)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[4];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 4, out ptrBytesRead);
            return BitConverter.ToSingle(bBuffer, 0);
        }
        public double ReadDouble(int iMemoryAddress, int[] iOffsets)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[8];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 8, out ptrBytesRead);
            return BitConverter.ToDouble(bBuffer, 0);
        }
        public string ReadText(int iMemoryAddress, int[] iOffsets, uint iStringLength, int iMode = 0)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[1];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, iStringLength, out ptrBytesRead);

            if (iMode == 0)
                return Encoding.UTF8.GetString(bBuffer);
            else if (iMode == 1)
                return BitConverter.ToString(bBuffer).Replace("-", "");
            else
                return "";
        }
        public byte[] ReadAOB(int iMemoryAddress, int[] iOffsets, uint iBytesToRead)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            byte[] bBuffer = new byte[1];
            IntPtr ptrBytesRead;
            MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, iBytesToRead, out ptrBytesRead);
            return bBuffer;
        }

        //Write to Pointer
        public bool Write(int iMemoryAddress, int[] iOffsets, byte bByteToWrite)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten;
            byte[] bBuffer = new byte[1] { bByteToWrite };
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 1, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == 1);
        }
        public bool Write(int iMemoryAddress, int[] iOffsets, short iShortToWrite)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten;
            byte[] bBuffer = BitConverter.GetBytes(iShortToWrite);
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 2, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == 2);
        }
        public bool Write(int iMemoryAddress, int[] iOffsets, int iIntToWrite)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten;
            byte[] bBuffer = BitConverter.GetBytes(iIntToWrite);
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 4, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == 4);
        }
        public bool Write(int iMemoryAddress, int[] iOffsets, long iLongToWrite)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten;
            byte[] bBuffer = BitConverter.GetBytes(iLongToWrite);
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 8, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == 8);
        }
        public bool Write(int iMemoryAddress, int[] iOffsets, float iFloatToWrite)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten;
            byte[] bBuffer = BitConverter.GetBytes(iFloatToWrite);
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 4, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == 4);
        }
        public bool Write(int iMemoryAddress, int[] iOffsets, double iDoubleToWrite)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten;
            byte[] bBuffer = BitConverter.GetBytes(iDoubleToWrite);
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, 8, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == 8);
        }
        public bool Write(int iMemoryAddress, int[] iOffsets, string sStringToWrite, int iMode = 0)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten; byte[] bBuffer = { 0 };

            if (iMode == 0)
                bBuffer = CreateAOBText(sStringToWrite);
            else if (iMode == 1)
                bBuffer = ReverseBytes(CreateAOBString(sStringToWrite));

            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBuffer, (uint)sStringToWrite.Length, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == sStringToWrite.Length);
        }
        public bool Write(int iMemoryAddress, int[] iOffsets, byte[] bBytesToWrite)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten;
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBytesToWrite, (uint)bBytesToWrite.Length, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == bBytesToWrite.Length);
        }
        public bool NOP(int iMemoryAddress, int[] iOffsets, int iLength)
        {
            int iFinalAddress = CalculatePointer(iMemoryAddress, iOffsets);
            IntPtr ptrBytesWritten; byte[] bBytesToWrite = new byte[iLength];
            for (int i = 0; i < iLength; i++) { bBytesToWrite[i] = 0x90; }
            MAPI.WriteProcessMemory(hReadProcess, (IntPtr)iFinalAddress, bBytesToWrite, (uint)iLength, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == bBytesToWrite.Length);
        }

        //Conversions
        public int Dec(int iHex)
        {
            return Int32.Parse(iHex.ToString(), NumberStyles.HexNumber);
        }
        public int Dec(string sHex)
        {
            return Int32.Parse(sHex, NumberStyles.HexNumber);
        }
        public string Hex(int iDec)
        {
            return iDec.ToString("X");
        }
        public string Hex(string sDec)
        {
            if (IsNumeric(sDec))
                return Int32.Parse(sDec).ToString("X");
            else
                return "0";
        }

        //Miscellaneous
        public bool BytesEqual(byte[] bBytes_1, byte[] bBytes_2)
        {
            return (BitConverter.ToString(bBytes_1) == BitConverter.ToString(bBytes_2));
        }
        public bool IsNumeric(string sNumber)
        {
            return new Regex(@"^\d+$").IsMatch(sNumber);
        }
        public byte[] ReverseBytes(byte[] bOriginalBytes)
        {
            int iBytes = bOriginalBytes.Length; byte[] bNewBytes = new byte[iBytes];

            for (int i = 0; i < iBytes; i++)
            {
                bNewBytes[iBytes - i - 1] = bOriginalBytes[i];
            }
            return bNewBytes;
        }

        //Miscellaneous Conversions
        private byte[] CreateAOBText(string sBytes)
        {
            return Encoding.ASCII.GetBytes(sBytes);
        }
        private byte[] CreateAOBString(string sBytes)
        {
            return BitConverter.GetBytes(Dec(sBytes));
        }
        private string CreateAddress(byte[] bBytes)
        {
            string sAddress = "";

            for (int i = 0; i < bBytes.Length; i++)
            {
                if (Convert.ToInt16(bBytes[i]) < 10)
                    sAddress = "0" + bBytes[i].ToString("X") + sAddress;
                else
                    sAddress = bBytes[i].ToString("X") + sAddress;
            }

            return sAddress;
        }

        //Calculate Pointer
        private int CalculatePointer(int iMemoryAddress, int[] iOffsets)
        {
            int iPointerCount = iOffsets.Length - 1; IntPtr ptrBytesRead; byte[] bBuffer = new byte[4]; int iTemporaryAddress = 0;

            if (iPointerCount == 0)
                iTemporaryAddress = iMemoryAddress;

            for (int i = 0; i <= iPointerCount; i++)
            {
                if (i == iPointerCount)
                {
                    MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iTemporaryAddress, bBuffer, 4, out ptrBytesRead);
                    iTemporaryAddress = Dec(CreateAddress(bBuffer)) + iOffsets[i];
                    return iTemporaryAddress;
                }
                else if (i == 0)
                {
                    MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesRead);
                    iTemporaryAddress = Dec(CreateAddress(bBuffer)) + iOffsets[0];
                }
                else
                {
                    MAPI.ReadProcessMemory(hReadProcess, (IntPtr)iTemporaryAddress, bBuffer, 4, out ptrBytesRead);
                    iTemporaryAddress = Dec(CreateAddress(bBuffer)) + iOffsets[i];
                }
            }
            return 0;
        }

        //Calculate Static Address
        public int CalculateStaticAddress(string sStaticOffset)
        {
            return BaseAddress() + Dec(sStaticOffset);
        }
        public int CalculateStaticAddress(int iStaticOffset)
        {
            return BaseAddress() + iStaticOffset;
        }
        public int CalculateStaticAddress(string sStaticOffset, string sModuleName)
        {
            return BaseAddress(sModuleName) + Dec(sStaticOffset);
        }
        public int CalculateStaticAddress(int iStaticOffset, string sModuleName)
        {
            return BaseAddress(sModuleName) + iStaticOffset;
        }
    }
}
