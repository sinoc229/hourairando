using NSShanghaiEXE.InputOutput.Audio;
using NSShanghaiEXE.InputOutput.Rendering;
using NSGame;
using System;

namespace NSEvent
{
    internal class EditItem : EventBase
    {
        public int number;
        private readonly bool get;
        private readonly bool message;

        public EditItem(IAudioEngine s, EventManager m, int q, bool get, SaveData save)
          : base(s, m, save)
        {
            this.NoTimeNext = true;
            this.get = get;
            this.number = q;
        }

        public override void Update()
        {
            if (this.get)
            {
                this.savedata.keyitem.Add(this.number);
                //Console.WriteLine(this.number);
                switch (this.number)
                {
                    case 3:
                        Console.WriteLine("Got metro pass (again)");
                        this.savedata.flagList[1653] = true;
                        break;

                    case 6:
                        Console.WriteLine("Got mari p-code");
                        this.savedata.flagList[148] = true;
                        break;

                    case 7:
                        Console.WriteLine("Got remi p-code");
                        this.savedata.flagList[1600] = true;
                        break;

                    case 8:
                        Console.WriteLine("Got rika p-code");
                        this.savedata.flagList[285] = true;
                        break;

                    case 9:
                        Console.WriteLine("Got tsubaki p-code");
                        this.savedata.flagList[166] = true;
                        break;

                    case 10:
                        Console.WriteLine("Got tenshi p-code (seemingly unused?)");
                        //this.savedata.flagList[166] = true;
                        break;

                    case 14:
                        Console.WriteLine("Got cruise ticket (again)");
                        this.savedata.flagList[1652] = true;
                        break;

                    case 19:
                        Console.WriteLine("Got wriggle pendant");
                        this.savedata.flagList[1654] = true;
                        break;

                    case 20:
                        Console.WriteLine("Got ROM ID");
                        this.savedata.flagList[396] = true;
                        break;


                    case 26:
                        Console.WriteLine("Got secret key");
                        this.savedata.flagList[857] = true;
                        break;

                    case 27:
                        Console.WriteLine("Got dark key");
                        this.savedata.flagList[1650] = true;
                        break;

                    case 28:
                        Console.WriteLine("Got Crimsondex");
                        this.savedata.flagList[880] = true;
                        break;

                    case 29:
                        Console.WriteLine("Got Troubleshooter data");
                        this.savedata.flagList[882] = true;
                        break;

                    case 30:
                        Console.WriteLine("Got Sword Data");
                        this.savedata.flagList[884] = true;
                        break;

                    case 31:
                        Console.WriteLine("Got Mirror Shard");
                        this.savedata.flagList[885] = true;
                        break;

                    case 32:
                        Console.WriteLine("Got FRZ core");
                        this.savedata.flagList[886] = true;
                        break;

                    case 33:
                        Console.WriteLine("Got Order book");
                        this.savedata.flagList[887] = true;
                        break;

                    default:
                        Console.WriteLine("Got unknown key item? (most likely job/sidequest related)");
                        Console.WriteLine(this.number);
                        break;
                }

            }
            else
                this.savedata.keyitem.RemoveAll(i => i == 99);
            this.EndCommand();
        }

        public override void SkipUpdate()
        {
            this.Update();
        }

        public override void Render(IRenderer dg)
        {
            this.NoTimesRender(dg);
        }
    }
}
