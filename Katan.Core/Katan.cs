﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Katan.Core.Extensions;

namespace Katan.Core
{
    public class Katan
    {
        #region Worker Fields
        protected static readonly int[] _IR = {
        1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1,
        0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0,
        1, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0,
        0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1,
        0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1,
        1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1,
        0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0,
        1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1,
        0, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 1,
        1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1,
        1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1,
        0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1,
        1, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0,
        0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1,
        0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1,
        1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0
        };
        protected static readonly int _key_bits = 80;
        protected static List<int> _key;
        protected List<int> _firstRegister;
        protected List<int> _secondRegister;
        protected int[] _setX;
        protected int[] _setY;
        protected int _firstRegisterCapacity;
        protected int _secondRegisterCapacity;
        protected Version _version;
        public Version KatanVersion
        {
            get => _version;
        }
        public int PublicKey;

        #endregion

        #region Katan Initialize Methods
        public Katan(Version version, int key)
        {
            PublicKey = key;
            _timer = new Stopwatch();
            _key = Cryptography.GenerateKey(key);
            switch (version)
            {
                case Version.Version32:
                    InitKatan32();
                    break;
                case Version.Version48:
                    InitKatan48();
                    break;
                case Version.Version64:
                    InitKatan64();
                    break;
                default:
                    InitKatan32();
                    break;
            }
        }


        protected void InitKatan32()
        {
            _version = Version.Version32;
            _firstRegister = new List<int>();
            _firstRegisterCapacity = 13;
            _secondRegister = new List<int>();
            _secondRegisterCapacity = 19;
            _setX = new int[5] { 12, 7, 8, 5, 3 };
            _setY = new int[6] { 18, 7, 12, 10, 8, 3 };
        }

        protected void InitKatan48()
        {
            _version = Version.Version48;
            _firstRegister = new List<int>();
            _firstRegisterCapacity = 19;
            _secondRegister = new List<int>();
            _secondRegisterCapacity = 29;
            _setX = new int[5] { 18, 12, 15, 7, 6 };
            _setY = new int[6] { 28, 19, 21, 13, 15, 6 };

        }

        protected void InitKatan64()
        {
            _version = Version.Version64;
            _firstRegister = new List<int>();
            _firstRegisterCapacity = 25;
            _secondRegister = new List<int>();
            _secondRegisterCapacity = 39;
            _setX = new int[5] { 24, 15, 20, 11, 9 };
            _setY = new int[6] { 38, 25, 33, 21, 14, 9 };

        }

        #endregion

        #region Katan Core Methods

        protected virtual void KatanRoundEncryption(int round)
        {
            int k_a = _key[2 * round];
            int k_b = _key[2 * round + 1];

            int f_a = _firstRegister[_setX[0]] ^ _firstRegister[_setX[1]]
                ^ (_firstRegister[_setX[2]] & _firstRegister[_setX[3]]) ^ k_a;
            if (_IR[round] != 0)
            {
                f_a ^= _firstRegister[_setX[4]];
            }

            int f_b = _secondRegister[_setY[0]] ^ _secondRegister[_setY[1]]
                ^ (_secondRegister[_setY[2]] & _secondRegister[_setY[3]])
                ^ (_secondRegister[_setY[4]] & _secondRegister[_setY[5]])
                ^ k_b;

            _firstRegister.RemoveAt(_firstRegisterCapacity - 1);
            _firstRegister.Insert(0, f_b);

            _secondRegister.RemoveAt(_secondRegisterCapacity - 1);
            _secondRegister.Insert(0, f_a);
        }

        public virtual List<int> KatanEncryption(List<int> plainTextBits)
        {
            _timer.Start();
            _secondRegister = plainTextBits.Take(_secondRegisterCapacity).ToList();
            _firstRegister = plainTextBits.Skip(_secondRegisterCapacity).ToList();
            for (int round = 0; round != 254; round++)
            {
                KatanRoundEncryption(round);
                if ((int)KatanVersion > 32)
                {
                    KatanRoundEncryption(round);
                }
                if ((int)KatanVersion > 48)
                {
                    KatanRoundEncryption(round);
                }
            }
            _timer.Stop();
            TotalEncryptionTime = _timer.Elapsed;
            _timer.Reset();
            return _secondRegister.Concat(_firstRegister).ToList();
        }

        public void KatanRoundDecryption(int round)
        {
            int k_a = _key[2 * round];
            int k_b = _key[2 * round + 1];

            int f_a = _secondRegister[0] ^ _firstRegister[_setX[1] + 1]
                ^ (_firstRegister[_setX[2] + 1] & _firstRegister[_setX[3] + 1])
                ^ k_a;
            if (_IR[round] != 0)
            {
                f_a ^= _firstRegister[_setX[4] + 1];
            }

            int f_b = _firstRegister[0] ^ _secondRegister[_setY[1] + 1]
                ^ (_secondRegister[_setY[2] + 1] & _secondRegister[_setY[3] + 1])
                ^ (_secondRegister[_setY[4] + 1] & _secondRegister[_setY[5] + 1])
                ^ k_b;

            _firstRegister.RemoveAt(0);
            _firstRegister.Insert(_firstRegisterCapacity - 1, f_a);

            _secondRegister.RemoveAt(0);
            _secondRegister.Insert(_secondRegisterCapacity - 1, f_b);
        }

        public List<int> KatanDecryption(List<int> cipherTextBits)
        {
            _timer.Start();
            _secondRegister = cipherTextBits.Take(_secondRegisterCapacity).ToList();
            _firstRegister = cipherTextBits.Skip(_secondRegisterCapacity).ToList();
            for (int round = 253; round != -1; round--)
            {
                KatanRoundDecryption(round);
                if ((int)KatanVersion > 32)
                {
                    KatanRoundDecryption(round);
                }
                if ((int)KatanVersion > 48)
                {
                    KatanRoundDecryption(round);
                }
            }
            _timer.Stop();
            TotalDecryptionTime = _timer.Elapsed;
            _timer.Reset();
            return _secondRegister.Concat(_firstRegister).ToList();
        }

        #endregion

        #region Infrastructure
        public enum Version
        {
            Version32 = 32,
            Version48 = 48,
            Version64 = 64
        }

        private Stopwatch _timer;
        public TimeSpan RoundEncryptionTime;
        public TimeSpan RoundDecryptionTime;
        public TimeSpan TotalEncryptionTime;
        public TimeSpan TotalDecryptionTime;
        #endregion
    }
}
