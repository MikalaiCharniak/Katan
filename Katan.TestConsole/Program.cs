using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katan.Core.Extensions;
using Katan.Core;
using System.IO;

namespace Katan.TestConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            StringBuilder sb;
            Katan.Core.Katan katan32 = new Katan.Core.Katan(Katan.Core.Katan.Version.Version32, 90);
            Katan.Core.Katan katan48 = new Katan.Core.Katan(Katan.Core.Katan.Version.Version48, 90);
            Katan.Core.Katan katan64 = new Katan.Core.Katan(Katan.Core.Katan.Version.Version64, 90);


            KatanTextAdapter katanTextAdapter32 = new KatanTextAdapter(katan32);
            KatanTextAdapter katanTextAdapter48 = new KatanTextAdapter(katan48);
            KatanTextAdapter katanTextAdapter64 = new KatanTextAdapter(katan64);

            var secretText32 = katanTextAdapter32.KatanEncryptText("hello my dear friend i really glad to see u today here, yea!)))09");
            var realText32 = katanTextAdapter32.AltKatanDecryptText(secretText32);
            Console.WriteLine(katanTextAdapter32.SpecialRetransformText(realText32));

            //var secretText48 = katanTextAdapter48.KatanEncryptText("hello my dear friend i really glad to see u today here, yea!)))09");
            //var realText48 = katanTextAdapter48.AltKatanDecryptText(secretText48);
            //Console.WriteLine(katanTextAdapter48.SpecialRetransformText(realText48));

            var secretText64 = katanTextAdapter64.KatanEncryptText("hello my dear friend i really glad to see u today here, yea!)))09");
            var realText64 = katanTextAdapter64.AltKatanDecryptText(secretText64);
            Console.WriteLine(katanTextAdapter64.SpecialRetransformText(realText64));
            Console.ReadKey();
        }
    }
}
