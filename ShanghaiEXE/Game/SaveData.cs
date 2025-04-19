using ExtensionMethods;
using NSAddOn;
using NSChip;
using NSShanghaiEXE.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Common.EncodeDecode;
using NSMap.Character.Menu;
using static NSMap.Character.Menu.Library;
using System.Linq;
using Common;
using System.Windows.Forms;
using NSMap;
using NSShanghaiEXE.Map;
using NSMap.Character;
using Archipelago;
using Archipelago.MultiClient.Net;

namespace NSGame
{

    public class SaveData
    {
        private const string FreeplayPath = "freeplay.she";
        private const string SavePath = "save.she";
        private const string SavePathTemp = "save.she.tmp";
        private const string BackupPath = "save.she.bak";

        public static int decCount = 0;
        public static string pass = "sasanasi";
        public static Virus[] HAVEVirus = new Virus[3];
        public static string[] EXmessID = 
        {
            "LHint1",
            "LHint2",
            "QHint1",
            "QHint2",
            "Omake1",
            "Omake2",
            "Omake3",
            "Omake4",
            "Qinfo",
            "QinfoEnd",
            "BBS1",
            "BBS2",
            "BBS3",
            "BBS4",
            "BBS5",
            "BBS6"
        };
        public static int[,] Pad = {{132,118,50,76,33,35,10,28,31,12,53,78},{100,102,101,103,3,2,4,5,7,6,-1,8}};
        public static bool ScreenMode = false;
        public bool saveEnd = true;
        private bool flugEnd = true;
        private bool valEnd = true;
        private bool shopEnd = true;
        private bool mysEnd = true;
        private bool ranEnd = true;
        private bool chipEnd = true;
        public byte[] busterspec = new byte[3];
        public bool[] havefolder = new bool[3];
        public bool[] regularflag = new bool[3];
        public byte[] regularchip = new byte[3];
        public int efolder = 0;
        private byte[,] havechip = new byte[450, 4];
        public List<ChipS> havechips = new List<ChipS>();
        public Style[] style = new Style[5];
        public int[] stylepoint = new int[6];
        public int[] haveSubChis = new int[7];
        public bool[] runSubChips = new bool[4];
        public int haveSubMemory = 2;
        private int naviFolder = 5;
        public int darkFolder = 1;
        public int plusFolder = 0;
        public int custom = 0;
        public List<string> addonNames = new List<string>();
        public List<AddOnBase> haveAddon = new List<AddOnBase>();
        public List<bool> equipAddon = new List<bool>();
        public byte[] time = new byte[4];
        public int manybattle = 0;
        private int money = 620;
        public int moneyover = 10000000;
        public int haveCaptureBomb = 0;
        private Virus[] haveVirus = new Virus[3];
        public List<Virus> stockVirus = new List<Virus>();
        public int[] havePeace = new int[3];
        public bool[,] bbsRead = new bool[6, 100];
        public bool[] questEnd = new bool[50];
        // flags for whether the current bounty target has been defeated
        public bool[] virusSPbustedFlug = new bool[45];
        // flags for whether the bounty targets have ever been defeated
        public bool[] virusSPbusted = new bool[45];
        public bool firstchange = false;
        public int[,,] chipFolder = new int[3, 30, 2];
        public bool[] canselectmenu = new bool[9];
        public bool[] datelist = new bool[450];
        public bool[] addonSkill = new bool[Enum.GetNames(typeof(SaveData.ADDONSKILL)).Length];
        public int[] netWorkName = new int[10];
        public List<int[]> RirekNetWorkName = new List<int[]>();
        public List<int> RirekNetWorkFace = new List<int>();
        public List<int> RirekNetWorkAddress = new List<int>();
        public List<int> mail = new List<int>();
        public List<bool> mailread = new List<bool>();
        public List<int> keyitem = new List<int>();
        private int[,] shopCount = new int[40, 10];
        public List<Interior> interiors = new List<Interior>();
        public bool[] flagList = new bool[2000];
        private VariableArray valList = new VariableArray();
        private bool[] getMystery = new bool[1000];
        private bool[] getRandomMystery = new bool[1000];
        public string pluginMap = "";
        public bool loadEnd;
        public bool loadSucces;
        public bool saveEndnowsub;
        public bool saveEndnow;
        private bool attemptingBackupLoad;
        private string fluglist;
        private string vallist;
        private string shoplist;
        private string myslist;
        private string ranlist;
        private string chiplist;
        public const int FolderMax = 30;
        public const int ManyChips_normal = 190;
        public const int ManyChips_navi = 64;
        public const int ManyChips_dark = 16;
        public const int ManyChips_PA = 32;
        public const int ManyMystery = 1000;
        public const int ManyChips = 450;
        public const int ManyNormalChips = 270;
        public const byte ManyCode = 4;
        public const byte ManyFolder = 3;
        public const byte Folder_chip = 0;
        public const byte Folder_code = 1;
        public const byte Chip_and_code = 2;
        public const byte ManyStyles = 6;
        public const byte TopmenuSelect = 9;
        public const int ManyFlags = 2000;
        public const int ManyVariables = 200;
        public const int BBSmany = 6;
        public const int BBSpages = 100;
        public const int QuestMany = 50;
        public const int SPVirusMany = 45;
        private int hpmax;
        public int HPnow;
        public int HPplus;
        public byte regularlarge;
        private Thread chipThread;
        public string foldername;
        public const byte canhavestyles = 5;
        public int havestyles;
        public int setstyle;
        public const int ManySubChips = 7;
        public Virus v;
        private int maxhz;
        private int maxcore;
        public string plase;
        public int mind;
        public int fukasinArea;
        public const int nameMany = 10;
        public int netWorkFace;
        public int message;
        public bool isJackedIn;
        public int selectQuestion;
        private Thread shopThread;
        private Thread flagThread;
        private Thread valThread;
        private Thread mysThread;
        private Thread ranThread;
        public string nowMap;
        public float nowX;
        public float nowY;
        public float nowZ;
        public int nowFroor;
        public float pluginX;
        public float pluginY;
        public float pluginZ;
        public int pluginFroor;
        public int steptype;
        public int stepoverX;
        public int stepoverY;
        public float stepCounter;
        public bool stepmode;
        public string item;
        public string category;
        //public VariableArray scrambleid = new VariableArray();

        //the spagheti is multiplying!
        //can probobly not have these be dclaired up here but if anyone thinks they can do it better
        //submit it, you coward
        public string[] scrambleid = new string[1000];
        public string[,] scrambleid2 = new string[1000,6];
        public int[,] scrambleidfinal = new int[1000, 6];
        public int[,] Randolist = new int[1000, 6];
        public int[,] Randolist2 = new int[1000, 6];
        public int[,] Randolistfinal = new int[1000, 6];

        public string[] scramblegifts2 = new string[1000];
        public string[] giftitemsglobal = new string[1000];


        public string[] mapnames = new string[1000];


        public int[,] apgive = new int[9999, 7];
        public int ap_itemq = 0;

        public UnboundedMap map;
        public RandomMystery[] randomMystery;

        public void Load(Control parent = null)
        {
            for (int i = 0; i < 9999; i++)
            {
                apgive[i, 6] = -1;

            }

            this.loadEnd = false;
            var loadAttemptedAndFailed = false;

            SaveData.decCount = 0;
            if (!File.Exists(SavePath))
            {
                this.loadSucces = false;
            }
            else
            {
                var streamReader = default(StreamReader);
                string str = "";
                try
                {
                    streamReader = new StreamReader(SavePath, Encoding.GetEncoding("Shift_JIS"));
                    
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray1 = str.Split('@');
                    this.addonNames.Clear();
                    if ((uint)strArray1.Length > 0U)
                    {
                        for (int index = 0; index < strArray1.Length - 1; ++index)
                            this.addonNames.Add(strArray1[index]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray2 = str.Split('@');
                    for (int index = 0; index < strArray2.Length - 1; ++index)
                        this.busterspec[index] = byte.Parse(strArray2[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray3 = str.Split('@');
                    for (int index = 0; index < strArray3.Length - 1; ++index)
                        this.canselectmenu[index] = bool.Parse(strArray3[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray4 = str.Split('/');
                    for (int index1 = 0; index1 < strArray4.Length - 1; ++index1)
                    {
                        string[] strArray5 = strArray4[index1].Split('|');
                        for (int index2 = 0; index2 < strArray5.Length - 1; ++index2)
                        {
                            string[] strArray6 = strArray5[index2].Split('@');
                            this.chipFolder[index1, index2, 0] = int.Parse(strArray6[0]);
                            this.chipFolder[index1, index2, 1] = int.Parse(strArray6[1]);
                        }
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray7 = str.Split('@');
                    this.havechips.Clear();
                    for (int index = 0; index < strArray7.Length - 1; ++index)
                        this.havechips.Add(new ChipS(int.Parse(strArray7[index].Split('/')[0]), int.Parse(strArray7[index].Split('/')[1])));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray8 = str.Split('@');
                    for (int index = 0; index < strArray8.Length - 1; ++index)
                        this.datelist[index] = bool.Parse(strArray8[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.efolder = byte.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray9 = str.Split('@');
                    this.equipAddon.Clear();
                    for (int index = 0; index < strArray9.Length - 1; ++index)
                        this.equipAddon.Add(bool.Parse(strArray9[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.firstchange = bool.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.foldername = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray10 = str.Split('@');
                    this.haveAddon.Clear();
                    for (int index = 0; index < strArray10.Length - 1; ++index)
                    {
                        string[] strArray5 = strArray10[index].Split('/');
                        var color = (AddOnBase.ProgramColor)Enum.Parse(typeof(AddOnBase.ProgramColor), strArray5[1]);
                        string typeName = strArray5[0].Split('.')[1];
                        var addOn = (AddOnBase)Activator.CreateInstance(typeName.ToAddOnType(), color);
                        this.haveAddon.Add(addOn);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.haveCaptureBomb = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray11 = str.Split('/');
                    for (int index1 = 0; index1 < strArray11.Length - 1; ++index1)
                    {
                        string[] strArray5 = strArray11[index1].Split('@');
                        for (int index2 = 0; index2 < strArray5.Length - 1; ++index2)
                            this.havechip[index1, index2] = byte.Parse(strArray5[index2]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray12 = str.Split('@');
                    for (int index = 0; index < strArray12.Length - 1; ++index)
                        this.havefolder[index] = bool.Parse(strArray12[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray13 = str.Split('@');
                    for (int index = 0; index < strArray13.Length - 1; ++index)
                        this.havePeace[index] = int.Parse(strArray13[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.havestyles = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray14 = str.Split('@');
                    for (int index = 0; index < strArray14.Length - 1; ++index)
                        this.haveSubChis[index] = int.Parse(strArray14[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.haveSubMemory = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray15 = str.Split('@');
                    for (int index = 0; index < strArray15.Length - 1; ++index)
                    {
                        if (strArray15[index] != "null")
                        {
                            string[] strArray5 = strArray15[index].Split('/');
                            this.HaveVirus[index] = new Virus
                            {
                                type = int.Parse(strArray5[0]),
                                eatBug = int.Parse(strArray5[1]),
                                eatError = int.Parse(strArray5[2]),
                                eatFreeze = int.Parse(strArray5[3]),
                                code = int.Parse(strArray5[4])
                            };
                        }
                        else
                            this.HaveVirus[index] = null;
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.HPmax = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.HPnow = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.HPplus = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray16 = str.Split('@');
                    this.keyitem.Clear();
                    for (int index = 0; index < strArray16.Length - 1; ++index)
                        this.keyitem.Add(int.Parse(strArray16[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray17 = str.Split('@');
                    this.mail.Clear();
                    for (int index = 0; index < strArray17.Length - 1; ++index)
                        this.mail.Add(int.Parse(strArray17[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray18 = str.Split('@');
                    this.mailread.Clear();
                    for (int index = 0; index < strArray18.Length - 1; ++index)
                        this.mailread.Add(bool.Parse(strArray18[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.manybattle = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.MaxCore = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.MaxHz = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.mind = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.Money = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.plase = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.isJackedIn = bool.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray19 = str.Split('@');
                    for (int index = 0; index < strArray19.Length - 1; ++index)
                        this.regularchip[index] = byte.Parse(strArray19[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray20 = str.Split('@');
                    for (int index = 0; index < strArray20.Length - 1; ++index)
                        this.regularflag[index] = bool.Parse(strArray20[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.Regularlarge = byte.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray21 = str.Split('@');
                    for (int index = 0; index < strArray21.Length - 1; ++index)
                        this.runSubChips[index] = bool.Parse(strArray21[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.selectQuestion = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.setstyle = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray22 = str.Split('@');
                    this.stockVirus.Clear();
                    for (int index = 0; index < strArray22.Length - 1; ++index)
                    {
                        string[] strArray5 = strArray22[index].Split('/');
                        this.stockVirus.Add(new Virus());
                        this.stockVirus[index].type = byte.Parse(strArray5[0]);
                        this.stockVirus[index].eatBug = byte.Parse(strArray5[1]);
                        this.stockVirus[index].eatError = byte.Parse(strArray5[2]);
                        this.stockVirus[index].eatFreeze = byte.Parse(strArray5[3]);
                        this.stockVirus[index].code = byte.Parse(strArray5[4]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray23 = str.Split('@');
                    for (int index = 0; index < strArray23.Length - 1; ++index)
                    {
                        string[] strArray5 = strArray23[index].Split('/');
                        this.style[index].style = int.Parse(strArray5[0]);
                        this.style[index].element = int.Parse(strArray5[1]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray24 = str.Split('@');
                    for (int index = 0; index < strArray24.Length - 1; ++index)
                        this.stylepoint[index] = int.Parse(strArray24[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray25 = str.Split('@');
                    for (int index = 0; index < strArray25.Length - 1; ++index)
                        this.time[index] = byte.Parse(strArray25[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray26 = str.Split('@');
                    for (int index = 0; index < strArray26.Length - 1; ++index)
                        this.virusSPbusted[index] = bool.Parse(strArray26[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray27 = str.Split('@');
                    for (int index = 0; index < strArray27.Length - 1; ++index)
                        this.virusSPbustedFlug[index] = bool.Parse(strArray27[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray28 = str.Split('@');
                    int index3 = -1;
                    for (int index1 = 0; index1 < this.bbsRead.GetLength(0); ++index1)
                    {
                        for (int index2 = 0; index2 < this.bbsRead.GetLength(1); ++index2)
                        {
                            ++index3;
                            this.bbsRead[index1, index2] = bool.Parse(strArray28[index3]);
                        }
                    }
                    try
                    {
                        bool flag = false;
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                        {
                            flag = true;
                            this.questEnd[index1] = bool.Parse(strArray5[index1]);
                        }
                        if (flag)
                            str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray6 = str.Split('/');
                        //this is shop delated, figgure out how it's zeroing them out
                        int index2 = -1;
                        for (int index1 = 0; index1 < this.shopCount.GetLength(0); ++index1)
                        {
                            for (int index4 = 0; index4 < this.shopCount.GetLength(1); ++index4)
                            {
                                ++index2;
                                this.shopCount[index1, index4] = int.Parse(strArray6[index2]);
                                //Console.WriteLine(this.shopCount[index2, index4]);
                            }
                        }
                    }
                    catch
                    {
                        string[] strArray5 = str.Split('/');
                        int index1 = -1;
                        for (int index2 = 0; index2 < this.shopCount.GetLength(0); ++index2)
                        {
                            for (int index4 = 0; index4 < this.shopCount.GetLength(1); ++index4)
                            {
                                ++index1;
                                this.shopCount[index2, index4] = int.Parse(strArray5[index1]);
                                //Console.WriteLine(this.shopCount[index2, index4]);
                            }
                        }
                    }
                    

                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.message = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.isJackedIn = bool.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowMap = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowX = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowY = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowZ = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowFroor = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.steptype = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.stepoverX = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.stepoverY = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.stepCounter = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginMap = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginX = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginY = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginZ = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginFroor = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray29 = str.Split('@');
                    for (int index1 = 0; index1 < strArray29.Length - 1; ++index1)
                    {
                        this.FlagList[index1] = bool.Parse(strArray29[index1]);
                        var flager = this.FlagList[index1];
                        string text = "flag num ";
                        //Console.WriteLine($"{text}{index1}:{flager}");
                    }
                str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray30 = str.Split('@');
                    for (int index1 = 0; index1 < strArray30.Length - 1; ++index1)
                    {
                        this.ValList[index1] = int.Parse(strArray30[index1]);
                        var valer = this.ValList[index1];
                        string text = "val num ";
                        //Console.WriteLine($"{text}{index1}:{valer}");
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray31 = str.Split('@');

                    

                    //Console.WriteLine(strArray31.Length - 1); //get total blue mystery data count
                    for (int index1 = 0; index1 < strArray31.Length - 1; ++index1)
                    {
                        this.GetMystery[index1] = bool.Parse(strArray31[index1]);

                    }

                    Console.WriteLine("Space alocated for G/B MD:");
                    Console.WriteLine(strArray31.Length - 1);

                    //this.scrambleid[index1] = index1;
                    //Array.Resize(ref this.scrambleid, 600);
                    //Console.WriteLine(this.scrambleid);
                    /* most likely not useable but keeping it in here just incase
                     * the initial idea was to offset what BMD's actually gave you, but that didn't work
                    try
                    {
                        for (int i = 0; i < strArray31.Length - 1; i++)
                        {
                            this.scrambleid[i] = i;
                        }
                        Console.WriteLine("scrumbob");
                    }
                    catch
                    {
                        Console.WriteLine("no scrumbo");
                    }
                   
                    int rng = ShanghaiEXE.Config.Seed;

                    Random random = new Random(rng);  // Create a Random object with the provided seed
                    int n = this.scrambleid.Length;

                    // Fisher-Yates shuffle algorithm (Knuth shuffle)
                    for (int i = n - 1; i > 0; i--)
                    {
                        // Pick a random index from 0 to i
                        int j = random.Next(i + 1);

                        // Swap array[i] with the element at random index j
                        int temp = this.scrambleid[i];
                        this.scrambleid[i] = this.scrambleid[j];
                        this.scrambleid[j] = temp;
                    }

                    Console.WriteLine("scramble results?");

                    Console.WriteLine(this.scrambleid[0]);
                    */



                    

                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray32 = str.Split('@');
                    for (int index1 = 0; index1 < strArray32.Length - 1; ++index1)
                    {
                        this.GetRandomMystery[index1] = bool.Parse(strArray32[index1]);
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length / 5; ++index1)
                            this.interiors.Add(new Interior(int.Parse(strArray5[index1 * 5]), int.Parse(strArray5[index1 * 5 + 1]), int.Parse(strArray5[index1 * 5 + 2]), bool.Parse(strArray5[index1 * 5 + 3]), bool.Parse(strArray5[index1 * 5 + 4])));
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                            this.netWorkName[index1] = int.Parse(strArray5[index1]);
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        this.netWorkFace = int.Parse(str);
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                            this.RirekNetWorkAddress.Add(int.Parse(strArray5[index1]));
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                            this.RirekNetWorkFace.Add(int.Parse(strArray5[index1]));
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                        {
                            string[] strArray6 = strArray5[index1].Split(',');
                            int[] numArray = new int[10];
                            for (int index2 = 0; index2 < numArray.Length; ++index2)
                                numArray[index2] = int.Parse(strArray6[index1]);
                            this.RirekNetWorkName.Add(numArray);
                        }
                    }
                    catch
                    {
                    }
                    this.canselectmenu[7] = false;
                    if (!this.flagList[553])
                    {
                        this.flagList[4] = false;
                        this.flagList[69] = false;
                    }

                    this.loadSucces = true;
                    this.shopThread = new Thread(new ThreadStart(this.ShopSave));
                    this.shopThread.Start();
                    this.flagThread = new Thread(new ThreadStart(this.FlugSave));
                    this.flagThread.Start();
                    this.valThread = new Thread(new ThreadStart(this.ValSave));
                    this.valThread.Start();
                    this.mysThread = new Thread(new ThreadStart(this.MysSave));
                    this.mysThread.Start();
                    this.ranThread = new Thread(new ThreadStart(this.RanSave));
                    this.ranThread.Start();
                    this.chipThread = new Thread(new ThreadStart(this.ChipSave));
                    this.chipThread.Start();
                    this.AddOnRUN();
                }
                catch
                {
                    this.loadSucces = false;
                    loadAttemptedAndFailed = true;
                }
                finally
                {
                    streamReader.Close();
                    streamReader.Dispose();
                }
            }

            if (!this.loadSucces && !this.attemptingBackupLoad)
            {
                var errorText = ShanghaiEXE.Translate("Save.MainSaveCorrupted").Text;

                if (File.Exists(BackupPath))
                {
                    File.Copy(BackupPath, SavePath, true);
                    this.attemptingBackupLoad = true;
                    this.Load(parent);
                    this.attemptingBackupLoad = false;

                    if (!this.loadSucces)
                    {
                        errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.BackupCorrupted").Text;
                    }
                    else
                    {
                        errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.BackupRestored").Text;
                    }

                    parent?.Invoke((Action)(() =>
                    {
                        MessageBox.Show(
                            errorText,
                            ShanghaiEXE.Translate("Save.MainSaveCorruptedTitle").Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }));
                }
                else if (loadAttemptedAndFailed)
                {
                    errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.BackupNotFound").Text;
                    
                    parent?.Invoke((Action)(() =>
                    {
                        MessageBox.Show(
                            errorText,
                            ShanghaiEXE.Translate("Save.MainSaveCorruptedTitle").Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }));
                }
            }


            Maplootfinder();

            AP_Connect();

            this.loadEnd = true;
        }

        public void LoadFreeplay(Control parent = null)
        {
            Console.WriteLine("Test!!!!");
            this.loadEnd = false;
            var loadAttemptedAndFailed = false;

            SaveData.decCount = 0;
            if (!File.Exists(FreeplayPath))
            {
                this.loadSucces = false;
            }
            else
            {
                var streamReader = default(StreamReader);
                string str = "";
                try
                {
                    streamReader = new StreamReader(FreeplayPath, Encoding.GetEncoding("Shift_JIS"));

                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray1 = str.Split('@');
                    this.addonNames.Clear();
                    if ((uint)strArray1.Length > 0U)
                    {
                        for (int index = 0; index < strArray1.Length - 1; ++index)
                            this.addonNames.Add(strArray1[index]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray2 = str.Split('@');
                    for (int index = 0; index < strArray2.Length - 1; ++index)
                        this.busterspec[index] = byte.Parse(strArray2[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray3 = str.Split('@');
                    for (int index = 0; index < strArray3.Length - 1; ++index)
                        this.canselectmenu[index] = bool.Parse(strArray3[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray4 = str.Split('/');
                    for (int index1 = 0; index1 < strArray4.Length - 1; ++index1)
                    {
                        string[] strArray5 = strArray4[index1].Split('|');
                        for (int index2 = 0; index2 < strArray5.Length - 1; ++index2)
                        {
                            string[] strArray6 = strArray5[index2].Split('@');
                            this.chipFolder[index1, index2, 0] = int.Parse(strArray6[0]);
                            this.chipFolder[index1, index2, 1] = int.Parse(strArray6[1]);
                        }
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray7 = str.Split('@');
                    this.havechips.Clear();
                    for (int index = 0; index < strArray7.Length - 1; ++index)
                        this.havechips.Add(new ChipS(int.Parse(strArray7[index].Split('/')[0]), int.Parse(strArray7[index].Split('/')[1])));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray8 = str.Split('@');
                    for (int index = 0; index < strArray8.Length - 1; ++index)
                        this.datelist[index] = bool.Parse(strArray8[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.efolder = byte.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray9 = str.Split('@');
                    this.equipAddon.Clear();
                    for (int index = 0; index < strArray9.Length - 1; ++index)
                        this.equipAddon.Add(bool.Parse(strArray9[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.firstchange = bool.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.foldername = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray10 = str.Split('@');
                    this.haveAddon.Clear();
                    for (int index = 0; index < strArray10.Length - 1; ++index)
                    {
                        string[] strArray5 = strArray10[index].Split('/');
                        var color = (AddOnBase.ProgramColor)Enum.Parse(typeof(AddOnBase.ProgramColor), strArray5[1]);
                        string typeName = strArray5[0].Split('.')[1];
                        var addOn = (AddOnBase)Activator.CreateInstance(typeName.ToAddOnType(), color);
                        this.haveAddon.Add(addOn);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.haveCaptureBomb = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray11 = str.Split('/');
                    for (int index1 = 0; index1 < strArray11.Length - 1; ++index1)
                    {
                        string[] strArray5 = strArray11[index1].Split('@');
                        for (int index2 = 0; index2 < strArray5.Length - 1; ++index2)
                            this.havechip[index1, index2] = byte.Parse(strArray5[index2]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray12 = str.Split('@');
                    for (int index = 0; index < strArray12.Length - 1; ++index)
                        this.havefolder[index] = bool.Parse(strArray12[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray13 = str.Split('@');
                    for (int index = 0; index < strArray13.Length - 1; ++index)
                        this.havePeace[index] = int.Parse(strArray13[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.havestyles = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray14 = str.Split('@');
                    for (int index = 0; index < strArray14.Length - 1; ++index)
                        this.haveSubChis[index] = int.Parse(strArray14[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.haveSubMemory = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray15 = str.Split('@');
                    for (int index = 0; index < strArray15.Length - 1; ++index)
                    {
                        if (strArray15[index] != "null")
                        {
                            string[] strArray5 = strArray15[index].Split('/');
                            this.HaveVirus[index] = new Virus
                            {
                                type = int.Parse(strArray5[0]),
                                eatBug = int.Parse(strArray5[1]),
                                eatError = int.Parse(strArray5[2]),
                                eatFreeze = int.Parse(strArray5[3]),
                                code = int.Parse(strArray5[4])
                            };
                        }
                        else
                            this.HaveVirus[index] = null;
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.HPmax = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.HPnow = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.HPplus = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray16 = str.Split('@');
                    this.keyitem.Clear();
                    for (int index = 0; index < strArray16.Length - 1; ++index)
                        this.keyitem.Add(int.Parse(strArray16[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray17 = str.Split('@');
                    this.mail.Clear();
                    for (int index = 0; index < strArray17.Length - 1; ++index)
                        this.mail.Add(int.Parse(strArray17[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray18 = str.Split('@');
                    this.mailread.Clear();
                    for (int index = 0; index < strArray18.Length - 1; ++index)
                        this.mailread.Add(bool.Parse(strArray18[index]));
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.manybattle = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.MaxCore = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.MaxHz = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.mind = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.Money = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.plase = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.isJackedIn = bool.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray19 = str.Split('@');
                    for (int index = 0; index < strArray19.Length - 1; ++index)
                        this.regularchip[index] = byte.Parse(strArray19[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray20 = str.Split('@');
                    for (int index = 0; index < strArray20.Length - 1; ++index)
                        this.regularflag[index] = bool.Parse(strArray20[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.Regularlarge = byte.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray21 = str.Split('@');
                    for (int index = 0; index < strArray21.Length - 1; ++index)
                        this.runSubChips[index] = bool.Parse(strArray21[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.selectQuestion = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.setstyle = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray22 = str.Split('@');
                    this.stockVirus.Clear();
                    for (int index = 0; index < strArray22.Length - 1; ++index)
                    {
                        string[] strArray5 = strArray22[index].Split('/');
                        this.stockVirus.Add(new Virus());
                        this.stockVirus[index].type = byte.Parse(strArray5[0]);
                        this.stockVirus[index].eatBug = byte.Parse(strArray5[1]);
                        this.stockVirus[index].eatError = byte.Parse(strArray5[2]);
                        this.stockVirus[index].eatFreeze = byte.Parse(strArray5[3]);
                        this.stockVirus[index].code = byte.Parse(strArray5[4]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray23 = str.Split('@');
                    for (int index = 0; index < strArray23.Length - 1; ++index)
                    {
                        string[] strArray5 = strArray23[index].Split('/');
                        this.style[index].style = int.Parse(strArray5[0]);
                        this.style[index].element = int.Parse(strArray5[1]);
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray24 = str.Split('@');
                    for (int index = 0; index < strArray24.Length - 1; ++index)
                        this.stylepoint[index] = int.Parse(strArray24[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray25 = str.Split('@');
                    for (int index = 0; index < strArray25.Length - 1; ++index)
                        this.time[index] = byte.Parse(strArray25[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray26 = str.Split('@');
                    for (int index = 0; index < strArray26.Length - 1; ++index)
                        this.virusSPbusted[index] = bool.Parse(strArray26[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray27 = str.Split('@');
                    for (int index = 0; index < strArray27.Length - 1; ++index)
                        this.virusSPbustedFlug[index] = bool.Parse(strArray27[index]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray28 = str.Split('@');
                    int index3 = -1;
                    for (int index1 = 0; index1 < this.bbsRead.GetLength(0); ++index1)
                    {
                        for (int index2 = 0; index2 < this.bbsRead.GetLength(1); ++index2)
                        {
                            ++index3;
                            this.bbsRead[index1, index2] = bool.Parse(strArray28[index3]);
                        }
                    }
                    try
                    {
                        bool flag = false;
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                        {
                            flag = true;
                            this.questEnd[index1] = bool.Parse(strArray5[index1]);
                        }
                        if (flag)
                            str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray6 = str.Split('/');
                        int index2 = -1;
                        for (int index1 = 0; index1 < this.shopCount.GetLength(0); ++index1)
                        {
                            for (int index4 = 0; index4 < this.shopCount.GetLength(1); ++index4)
                            {
                                ++index2;
                                this.shopCount[index1, index4] = int.Parse(strArray6[index2]);
                            }
                        }
                    }
                    catch
                    {
                        string[] strArray5 = str.Split('/');
                        int index1 = -1;
                        for (int index2 = 0; index2 < this.shopCount.GetLength(0); ++index2)
                        {
                            for (int index4 = 0; index4 < this.shopCount.GetLength(1); ++index4)
                            {
                                ++index1;
                                this.shopCount[index2, index4] = int.Parse(strArray5[index1]);
                            }
                        }
                    }
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.message = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.isJackedIn = bool.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowMap = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowX = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowY = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowZ = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.nowFroor = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.steptype = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.stepoverX = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.stepoverY = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.stepCounter = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginMap = str;
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginX = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginY = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginZ = FloatParseAnySeparator(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    this.pluginFroor = int.Parse(str);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray29 = str.Split('@');
                    for (int index1 = 0; index1 < strArray29.Length - 1; ++index1)
                        this.FlagList[index1] = bool.Parse(strArray29[index1]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray30 = str.Split('@');
                    for (int index1 = 0; index1 < strArray30.Length - 1; ++index1)
                        this.ValList[index1] = int.Parse(strArray30[index1]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray31 = str.Split('@');
                    for (int index1 = 0; index1 < strArray31.Length - 1; ++index1)
                        this.GetMystery[index1] = bool.Parse(strArray31[index1]);
                    str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                    string[] strArray32 = str.Split('@');
                    for (int index1 = 0; index1 < strArray32.Length - 1; ++index1)
                        this.GetRandomMystery[index1] = bool.Parse(strArray32[index1]);
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length / 5; ++index1)
                            this.interiors.Add(new Interior(int.Parse(strArray5[index1 * 5]), int.Parse(strArray5[index1 * 5 + 1]), int.Parse(strArray5[index1 * 5 + 2]), bool.Parse(strArray5[index1 * 5 + 3]), bool.Parse(strArray5[index1 * 5 + 4])));
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                            this.netWorkName[index1] = int.Parse(strArray5[index1]);
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        this.netWorkFace = int.Parse(str);
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                            this.RirekNetWorkAddress.Add(int.Parse(strArray5[index1]));
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                            this.RirekNetWorkFace.Add(int.Parse(strArray5[index1]));
                    }
                    catch
                    {
                    }
                    try
                    {
                        str = TCDEncodeDecode.DecryptString(streamReader.ReadLine(), SaveData.pass);
                        string[] strArray5 = str.Split('@');
                        for (int index1 = 0; index1 < strArray5.Length - 1; ++index1)
                        {
                            string[] strArray6 = strArray5[index1].Split(',');
                            int[] numArray = new int[10];
                            for (int index2 = 0; index2 < numArray.Length; ++index2)
                                numArray[index2] = int.Parse(strArray6[index1]);
                            this.RirekNetWorkName.Add(numArray);
                        }
                    }
                    catch
                    {
                    }
                    this.canselectmenu[7] = false;
                    if (!this.flagList[553])
                    {
                        this.flagList[4] = false;
                        this.flagList[69] = false;
                    }

                    this.loadSucces = true;
                    this.shopThread = new Thread(new ThreadStart(this.ShopSave));
                    this.shopThread.Start();
                    this.flagThread = new Thread(new ThreadStart(this.FlugSave));
                    this.flagThread.Start();
                    this.valThread = new Thread(new ThreadStart(this.ValSave));
                    this.valThread.Start();
                    this.mysThread = new Thread(new ThreadStart(this.MysSave));
                    this.mysThread.Start();
                    this.ranThread = new Thread(new ThreadStart(this.RanSave));
                    this.ranThread.Start();
                    this.chipThread = new Thread(new ThreadStart(this.ChipSave));
                    this.chipThread.Start();
                    this.AddOnRUN();
                }
                catch
                {
                    this.loadSucces = false;
                    loadAttemptedAndFailed = true;
                }
                finally
                {
                    streamReader.Close();
                    streamReader.Dispose();
                }
            }

            if (!this.loadSucces && !this.attemptingBackupLoad)
            {
                var errorText = ShanghaiEXE.Translate("Save.MainSaveCorrupted").Text;

                if (File.Exists(BackupPath))
                {
                    File.Copy(BackupPath, FreeplayPath, true);
                    this.attemptingBackupLoad = true;
                    this.Load(parent);
                    this.attemptingBackupLoad = false;

                    if (!this.loadSucces)
                    {
                        errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.BackupCorrupted").Text;
                    }
                    else
                    {
                        errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.BackupRestored").Text;
                    }

                    parent?.Invoke((Action)(() =>
                    {
                        MessageBox.Show(
                            errorText,
                            ShanghaiEXE.Translate("Save.MainSaveCorruptedTitle").Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }));
                }
                else if (loadAttemptedAndFailed)
                {
                    errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.BackupNotFound").Text;

                    parent?.Invoke((Action)(() =>
                    {
                        MessageBox.Show(
                            errorText,
                            ShanghaiEXE.Translate("Save.MainSaveCorruptedTitle").Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }));
                }
            }

            this.loadEnd = true;

            

        } //not used

        public ICollection<Dialogue> RetconSave()
        {
            var retconMessages = new List<Dialogue>();

            // 0 : unmodified, fix hospital event incident BGM not cleared.
            if (this.ValList[199] <= 0)
            {
                // If hospital event complete, the postgame hasn't started, and endgame robots aren't out 
                if (this.ValList[14] != 0 && this.FlagList[744] && !this.FlagList[791] && this.ValList[10] != 7)
                {
                    this.ValList[14] = 0;
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0503HospitalEmergencyMusic"));
                }
            }

            // 1: 0.550, fix chip ID issues, add illegal chips to library (only recordkeeping), set new hint message
            // Refund duplicate addons
            if (this.ValList[199] <= 1)
            {
                var replacements = new[]
                {
                    new { Original = 416, New = 253 },
                    new { Original = 417, New = 254 },
                    new { Original = 266, New = 313 }
                };
                // Replace in-folder chips
                for (var folderIndex = 0; folderIndex < this.chipFolder.GetLength(0); folderIndex++)
                {
                    for (var chipIndex = 0; chipIndex < this.chipFolder.GetLength(1); chipIndex++)
                    {
                        foreach (var replacement in replacements)
                        {
                            if (this.chipFolder[folderIndex, chipIndex, 0] == replacement.Original)
                            {
                                this.chipFolder[folderIndex, chipIndex, 0] = replacement.New;
                            }
                        }
                    }
                }
                // Replace in-bag chip counts
                for (var codeIndex = 0; codeIndex < 4; codeIndex++)
                {
                    foreach (var replacement in replacements)
                    {
                        this.havechip[replacement.New, codeIndex] = this.havechip[replacement.Original, codeIndex];
                        this.havechip[replacement.Original, codeIndex] = 0;
                    }
                }
                // Replace bag order chips
                for (var chipIndex = 0; chipIndex < this.havechips.Count; chipIndex++)
                {
                    foreach (var replacement in replacements)
                    {
                        if (this.havechips[chipIndex].number == replacement.Original)
                        {
                            this.havechips[chipIndex] = new ChipS(replacement.New, this.havechips[chipIndex].code);
                        }
                    }
                }

                // Add illegal chips to seen list
                for (var chipIndex = 310; chipIndex < this.havechip.GetLength(0); chipIndex++)
                {
                    if (this.havechip[chipIndex, 0] > 0 || this.havechip[chipIndex, 1] > 0 || this.havechip[chipIndex, 2] > 0 || this.havechip[chipIndex, 3] > 0)
                    {
                        this.datelist[chipIndex - 1] = true;
                    }
                }

                // Add/remove chips from seen list
                foreach (var replacement in replacements)
                {
                    this.datelist[replacement.New - 1] = this.datelist[replacement.Original - 1];
                    this.datelist[replacement.Original - 1] = false;
                }

                // Change Humor, EirinCall to grey
                var replacedAddonNames = string.Empty;
                for (int i = 0; i < this.haveAddon.Count; i++)
                {
                    var addOn = this.haveAddon[i];
                    if (addOn.ID == 54 || addOn.ID == 57)
                    {
                        this.haveAddon[i].color = AddOnBase.ProgramColor.glay;
                        if (string.IsNullOrEmpty(replacedAddonNames))
                        {
                            replacedAddonNames = addOn.name;
                        }
                        else
                        {
                            replacedAddonNames = $"{replacedAddonNames} and {addOn.name}";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(replacedAddonNames))
                {
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550HumorEirinCallFormat").Format(replacedAddonNames));
                }
                
                var refundedAddons = new List<Tuple<int, int, string>>();
                var refundedAddonNames = new List<string>();
                var voileCRecovBought = this.shopCount[11, 8] > 0;
                var voileCShotgunBought = this.shopCount[11, 9] > 0;
                var undersquareCLanceBought = this.shopCount[14, 2] > 0;
                var undernetLostLghtBought = this.shopCount[15, 1] > 0;
                var engellesFullOpenOpened = this.getMystery[149];

                // Bought CRecov from Voile for 12000 Z (removed, now only from Engelles 1 BMD 136)
                if (voileCRecovBought)
                {
                    refundedAddons.Add(Tuple.Create(92, 12000, "Z"));
                }

                // Bought CShotgun from Voile for 12000 Z (removed, now only bought from Undersquare 2 for 17000 Z)
                if (voileCShotgunBought)
                {
                    // No refund, free 5000 Z and mark down Undersquare 2
                    this.shopCount[14, 2] = 1;
                }

                // Bought CLance from World Undersquare for 17000 Z (replaced with CShotgun, now only at Voile)
                if (undersquareCLanceBought)
                {
                    refundedAddons.Add(Tuple.Create(91, 17000, "Z"));
                }

                // Bought LostLght from Undernet 3 for 50 BugFrags (removed, now only bought from LordUsa comp for 18000 Z)
                if (undernetLostLghtBought)
                {
                    refundedAddons.Add(Tuple.Create(73, 50, "BugFrag"));
                    for (var i = 1; i <= 4; i++)
                    {
                        this.shopCount[15, i] = this.shopCount[15, i + 1];
                    }
                }

                // Got FullOpen from Engelles 3 PMD (replaced with MedusEye Y, now only from Undernet 10 BMD behind ROM gate)
                if (engellesFullOpenOpened)
                {
                    refundedAddons.Add(Tuple.Create(20, 0, ""));
                    this.AddChip(10, 3, true);
                }

                var removedIndices = new List<Tuple<int,int>>();
                for (var i = 0; i < this.haveAddon.Count; i++)
                {
                    var addOn = this.haveAddon[i];
                    var refund = refundedAddons.FirstOrDefault(tup => tup.Item1 == addOn.ID);
                    if (refund != null)
                    {
                        refundedAddons.Remove(refund);
                        refundedAddonNames.Add(addOn.name);

                        removedIndices.Add(Tuple.Create(i, this.equipAddon[i] ? this.equipAddon.Take(i).Count(b => b) : -1));
                        switch (refund.Item3)
                        {
                            case "Z":
                                this.money += refund.Item2;
                                break;
                            case "BugFrag":
                                this.havePeace[0] += refund.Item2;
                                break;
                            case "":
                                break;
                        }
                    }
                }

                foreach (var removedIndex in removedIndices.OrderByDescending(ri => ri.Item1))
                {
                    this.haveAddon.RemoveAt(removedIndex.Item1);
                    this.equipAddon.RemoveAt(removedIndex.Item1);
                    if (removedIndex.Item2 != -1)
                    {
                        this.addonNames.RemoveAt(removedIndex.Item2);
                    }
                }

                if (refundedAddonNames.Any())
                {
                    var refundedAddonsString = refundedAddonNames.Aggregate((accum, next) =>
                    {
                        var entries = accum.Count(c => c == '，') + 1;
                        var linebreak = entries != 0 && entries % 3 == 0 ? "," : string.Empty;
                        return $"{accum}，{linebreak}{next}";
                    });
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550AddOnRefundFormat").Format(refundedAddonsString));
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550AddOnRefund2"));
                    this.AddOnRUN();
                }

                // Fix old "at end of game" L message with postgame message
                if (this.ValList[3] == 101)
                {
                    this.ValList[3] = 102;
                }

                // Guess V3 defeat flags from V2 flags and
                // V2, chip, code, V3, name
                var v2AndChips = new[]
                {
                    new {V2 = 35, Chip = 220, Code = 0, V3 = 838, Name="Enemy.CirnoName4"}, // Cirno
                    new {V2 = 165, Chip = 229, Code = 0, V3 = 839, Name="Enemy.PyroManName4"}, // PyroMan
                    new {V2 = 170, Chip = 232, Code = 0, V3 = 840, Name="Enemy.MrasaName4"}, // Murasa
                    new {V2 = 171, Chip = 235, Code = 0, V3 = 841, Name="Enemy.ScissorManName4"}, // ScissorMan
                    new {V2 = 173, Chip = 238, Code = 0, V3 = 842, Name="Enemy.ChenName4"}, // Chen
                    new {V2 = 790, Chip = 247, Code = 0, V3 = 843, Name="Enemy.DruidManNameV3"}, // DruidMan
                    new {V2 = 148, Chip = 193, Code = 0, V3 = 844, Name="Enemy.MarisaName3"}, // Marisa
                    new {V2 = 155, Chip = 196, Code = 0, V3 = 845, Name="Enemy.SakuyaName3"}, // Sakuya
                    new {V2 = 163, Chip = 199, Code = 0, V3 = 846, Name="Enemy.TankManName3"}, // TankMan
                    new {V2 = 164, Chip = 226, Code = 0, V3 = 847, Name="Enemy.IkuName3"}, // Iku
                    new {V2 = 166, Chip = 202, Code = 0, V3 = 848, Name="Enemy.SpannerManName3"}, // SpannerMan
                    new {V2 = 161, Chip = 223, Code = 0, V3 = 849, Name="Enemy.MedicineName3"}, // Medicine
                    new {V2 = 169, Chip = 217, Code = 0, V3 = 850, Name="Enemy.YorihimeName3"}, // Yorihime
                    new {V2 = 168, Chip = 208, Code = 0, V3 = 851, Name="Enemy.HakutakuManName3"}, // HakutakuMan
                    new {V2 = 167, Chip = 211, Code = 0, V3 = 852, Name="Enemy.TortoiseManName3"}, // TortoiseMan
                    new {V2 = 172, Chip = 214, Code = 0, V3 = 853, Name="Enemy.BeetleManName3"}, // BeetleMan
                    new {V2 = 174, Chip = 241, Code = 0, V3 = 854, Name="Enemy.RanName3"}, // Ran
                    // No implemented ways to fight Youmu, Utsuho
                };

                var v3Flags = v2AndChips.ToList();
                v3Flags.Clear();

                foreach (var check in v2AndChips)
                {
                    var v2Flag = this.FlagList[check.V2];
                    var hasChip = false;
                    for (var codeIndex = 0; codeIndex < 4; codeIndex++)
                    {
                        hasChip |= this.havechip[check.Chip, check.Code] != 0;
                    }
                    for (var folderIndex = 0; folderIndex < this.chipFolder.GetLength(0); folderIndex++)
                    {
                        for (var chipIndex = 0; chipIndex < this.chipFolder.GetLength(1); chipIndex++)
                        {
                            hasChip |= this.chipFolder[folderIndex, chipIndex, 0] == check.Chip
                                && this.chipFolder[folderIndex, chipIndex, 1] == check.Code;
                        }
                    }

                    if (v2Flag && hasChip)
                    {
                        v3Flags.Add(check);
                    }
                }

                if (v3Flags.Any())
                {
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550V3Tracking"));
                    var nameDialogue = default(Dialogue);
                    var lines = 0;
                    foreach (var v3 in v3Flags)
                    {
                        this.FlagList[v3.V3] = true;

                        if (lines == 0)
                        {
                            nameDialogue = new Dialogue { Face = FACE.Sprite.ToFaceId(), Text = ShanghaiEXE.Translate(v3.Name) + ",," };
                            lines++;
                        }
                        else
                        {
                            var blankCommas = 3 - lines;
                            nameDialogue.Text = nameDialogue.Text.Substring(0, nameDialogue.Text.Length - blankCommas)
                                + "," + ShanghaiEXE.Translate(v3.Name)
                                + new string(Enumerable.Repeat(',', blankCommas - 1).ToArray());
                            lines++;
                            if (lines >= 3)
                            {
                                retconMessages.Add(nameDialogue);
                                nameDialogue = null;
                                lines = 0;
                            }
                        }
                    }

                    if (nameDialogue != null)
                    {
                        retconMessages.Add(nameDialogue);
                    }
                }

                // Lloyd -> Troid retcon
                if (retconMessages.Any())
                {
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550Lloyd"));
                }
            }

            if (this.ValList[199] <= 2)
            {
                // Shift City BBS ids up by 3 for new entries
                var cityBbsEntries = this.bbsRead.GetLength(1);
                var movedEntriesRead = Enumerable.Range(19, cityBbsEntries - 19).Any(i => this.bbsRead[1, i]);
                if (movedEntriesRead)
                {
                    for (var i = cityBbsEntries - 1; i > 21; i--)
                    {
                        this.bbsRead[1, i] = this.bbsRead[1, i - 3];
                    }
                    this.bbsRead[1, 19] = false;
                    this.bbsRead[1, 20] = false;
                    this.bbsRead[1, 21] = false;
                }

                // If HeavenNet already entered, warn that area in progress, battles to be reverted
                // TODO: when no longer needed (battles implemented), unset 900 & revert battles (+give message)
                //if (this.FlagList[793] && !this.FlagList[900])
                //{
                //    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550HeavenWIP2"));
                //    this.FlagList[900] = true;
                //}
            }

            if (this.ValList[199] <= 3)
            {
                var reAddedAddOns = new List<string>();

                var engellesCRecovOpened = this.getMystery[136];
                if (engellesCRecovOpened && !this.haveAddon.Any(ao => ao is CRepair))
                {
                    this.GetAddon(new CRepair(AddOnBase.ProgramColor.blue));
                    reAddedAddOns.Add(ShanghaiEXE.Translate("AddOn.CRepairName"));
                }

                var undersquareCShotgunBought = this.shopCount[14, 1] > 0;
                if (undersquareCShotgunBought && !this.haveAddon.Any(ao => ao is CShotGun))
                {
                    this.GetAddon(new CShotGun(AddOnBase.ProgramColor.blue));
                    reAddedAddOns.Add(ShanghaiEXE.Translate("AddOn.CShotgunName"));
                }

                if (reAddedAddOns.Any())
                {
                    var reAddDialogue = ShanghaiEXE.Translate("Retcon.0550ReAddedAddOns");
                    reAddDialogue.Text += string.Join("，", reAddedAddOns);
                    retconMessages.Add(reAddDialogue);
                }
            }

            // WIP9
            if (this.ValList[199] <= 4)
            {
                // Replace new addons
                var hasFriendship = this.haveAddon.Any(ao => ao is Sacrifice);
                var hasMammon = this.haveAddon.Any(ao => ao is Mammon);
                var givNTakeIndex = this.haveAddon.FindIndex(ao => ao is Yuzuriai);
                if (givNTakeIndex != -1)
                {
                    var equipIndex = this.equipAddon[givNTakeIndex];
                    var equipNameList = equipIndex ? this.equipAddon.Take(givNTakeIndex).Count(b => b) : -1;

                    this.haveAddon.RemoveAt(givNTakeIndex);
                    this.equipAddon.RemoveAt(givNTakeIndex);

                    if (equipNameList != -1)
                    {
                        this.addonNames.RemoveAt(equipNameList);
                    }

                    this.GetAddon(new Yuzuriai(AddOnBase.ProgramColor.gleen));
                }
                if (hasFriendship || hasMammon)
                {
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550AddOnRebalance"));
                }
                if (givNTakeIndex != -1)
                {
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550AddOnRebalance2"));
                }

                if (this.FlagList[796])
                {
                    this.FlagList[795] = true;
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550SageSkipped"));
                }
                
                if (this.FlagList[880])
                {
                    this.FlagList[800] = true;
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550CrimDexEnabled"));
                }
            }

            // WIP10
            if (this.ValList[199] <= 5)
            {
                // Reset flag accidentally left on after cutscene
                // Flag13 should only be active during automatically-progressing cutscenes, no savegame should have it set legitimately
                this.FlagList[13] = false;

                // Reset WIP endgame flags
                var endgamePlaceholderFlags = new[]
                {
                    803, 804, 805, 806, 807, 808, 809, 824, 825, 826, 827, // Ghost doors opened (always linked to ghosts defeated)
                    814, 815, 816, 817, 818, 819, 820, 828, 829, 830, 831, // Ghosts defeated
                    822, 861, 823, 832, 835, 833, 837 // Barriers destroyed (barrier doors opening unaffected)
                };
                var anyFlagsReset = false;
                foreach (var endgameFlag in endgamePlaceholderFlags)
                {
                    if (this.FlagList[endgameFlag])
                    {
                        this.FlagList[endgameFlag] = false;
                        anyFlagsReset = true;
                    }
                }
                if (anyFlagsReset)
                {
                    retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550WIPEndgameReset"));

                    if (this.nowMap == "heavenNet1" || this.nowMap == "heavenNet2")
                    {
                        retconMessages.Add(ShanghaiEXE.Translate("Retcon.0550HeavenJackOut"));

                        this.nowMap = this.pluginMap;
                        this.nowFroor = this.pluginFroor;
                        this.nowX = this.pluginX;
                        this.nowY = this.pluginY;
                        this.nowZ = this.pluginZ;

                        this.isJackedIn = false;
                        this.FlagList[2] = false;
                        if (!this.FlagList[13])
                        {
                            this.GetRandomMystery = new bool[600];
                            this.runSubChips[0] = false;
                            this.runSubChips[1] = false;
                            this.runSubChips[2] = false;
                            this.runSubChips[3] = false;
                            this.ValList[19] = 0;
                            this.HPNow = this.HPMax;
                            this.steptype = (int)SceneMap.STEPS.normal;
                        }
                    }
                }

                // Unset WIP message shown flag
                this.FlagList[900] = false;
            }

            // WIP14
            if (this.ValList[199] <= 6)
            {
                // Remove duplicate keys (only if door opened)
                if (this.keyitem.Count(ki => ki == 26) >= 2)
                {
                    this.keyitem.RemoveAll(ki => ki == 26);
                }

                // Replace demo room with interior
                if (this.ValList[10] >= 8)
                {
                    this.interiors.Add(new Interior(51, 106, 186, true, false));
                    this.FlagList[465] = true;
                }
            }

            // WIP15
            if (this.ValList[199] <= 7)
            {
                // Return demo and debug rooms if discarded
                if (this.FlagList[465] && !this.interiors.Any(i => i.number == 51))
                {
                    this.interiors.Add(new Interior(51, 106, 186, false, false));
                }
                if (this.FlagList[466] && !this.interiors.Any(i => i.number == 52))
                {
                    this.interiors.Add(new Interior(52, 136, 186, false, false));
                }
            }

            // Set var to "current save version"
            this.ValList[199] = 8;
            return retconMessages;
        }

        public void SaveFile(Form parent = null)
        {
            var streamWriter = default(StreamWriter);
            var saveFailed = false;

            try
            {
                this.shopThread = new Thread(new ThreadStart(this.ShopSave));
                this.shopThread.Start();
                this.flagThread = new Thread(new ThreadStart(this.FlugSave));
                this.flagThread.Start();
                this.valThread = new Thread(new ThreadStart(this.ValSave));
                this.valThread.Start();
                this.mysThread = new Thread(new ThreadStart(this.MysSave));
                this.mysThread.Start();
                this.ranThread = new Thread(new ThreadStart(this.RanSave));
                this.ranThread.Start();
                this.chipThread = new Thread(new ThreadStart(this.ChipSave));
                this.chipThread.Start();
                this.saveEnd = false;
                StringBuilder stringBuilder = new StringBuilder();
                streamWriter = new StreamWriter(SavePathTemp, false, Encoding.GetEncoding("Shift_JIS"));
                StringBuilder sourceString1 = new StringBuilder();
                foreach (string addonName in this.addonNames)
                    sourceString1.Append(addonName + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString1, SaveData.pass));
                StringBuilder sourceString2 = new StringBuilder();
                foreach (byte num in this.busterspec)
                    sourceString2.Append(((int)num).ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString2, SaveData.pass));
                StringBuilder sourceString3 = new StringBuilder();
                foreach (bool flag in this.canselectmenu)
                    sourceString3.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString3, SaveData.pass));
                StringBuilder sourceString4 = new StringBuilder();
                for (int index1 = 0; index1 < this.chipFolder.GetLength(0); ++index1)
                {
                    for (int index2 = 0; index2 < this.chipFolder.GetLength(1); ++index2)
                    {
                        for (int index3 = 0; index3 < this.chipFolder.GetLength(2); ++index3)
                            sourceString4.Append(this.chipFolder[index1, index2, index3].ToString() + "@");
                        sourceString4.Append("|");
                    }
                    sourceString4.Append("/");
                }
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString4, SaveData.pass));
                StringBuilder sourceString5 = new StringBuilder();
                foreach (ChipS havechip in this.havechips)
                    sourceString5.Append(havechip.number.ToString() + "/" + havechip.code + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString5, SaveData.pass));
                StringBuilder sourceString6 = new StringBuilder();
                foreach (bool flag in this.datelist)
                    sourceString6.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString6, SaveData.pass));
                StringBuilder sourceString7 = new StringBuilder();
                sourceString7.Append(this.efolder);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString7, SaveData.pass));
                StringBuilder sourceString8 = new StringBuilder();
                foreach (bool flag in this.equipAddon)
                    sourceString8.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString8, SaveData.pass));
                StringBuilder sourceString9 = new StringBuilder();
                sourceString9.Append(this.firstchange);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString9, SaveData.pass));
                StringBuilder sourceString10 = new StringBuilder();
                sourceString10.Append(this.foldername);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString10, SaveData.pass));
                StringBuilder sourceString11 = new StringBuilder();
                foreach (AddOnBase addOnBase in this.haveAddon)
                {
                    var typeName = addOnBase.GetType().ToAddOnName();
                    sourceString11.Append(typeName + "/" + addOnBase.color + "@");
                }
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString11, SaveData.pass));
                StringBuilder sourceString12 = new StringBuilder();
                sourceString12.Append(this.haveCaptureBomb);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString12, SaveData.pass));
                this.chipThread.Join();
                streamWriter.WriteLine(this.chiplist);
                StringBuilder sourceString13 = new StringBuilder();
                foreach (bool flag in this.havefolder)
                    sourceString13.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString13, SaveData.pass));
                StringBuilder sourceString14 = new StringBuilder();
                foreach (int num in this.havePeace)
                    sourceString14.Append(num.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString14, SaveData.pass));
                StringBuilder sourceString15 = new StringBuilder();
                sourceString15.Append(this.havestyles);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString15, SaveData.pass));
                StringBuilder sourceString16 = new StringBuilder();
                foreach (int haveSubChi in this.haveSubChis)
                    sourceString16.Append(haveSubChi.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString16, SaveData.pass));
                StringBuilder sourceString17 = new StringBuilder();
                sourceString17.Append(this.haveSubMemory);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString17, SaveData.pass));
                StringBuilder sourceString18 = new StringBuilder();
                foreach (Virus haveViru in this.HaveVirus)
                {
                    if (haveViru != null)
                        sourceString18.Append(haveViru.type.ToString() + "/" + haveViru.eatBug + "/" + haveViru.eatError + "/" + haveViru.eatFreeze + "/" + haveViru.code + "@");
                    else
                        sourceString18.Append("null@");
                }
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString18, SaveData.pass));
                StringBuilder sourceString19 = new StringBuilder();
                sourceString19.Append(this.HPmax);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString19, SaveData.pass));
                StringBuilder sourceString20 = new StringBuilder();
                sourceString20.Append(this.HPnow);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString20, SaveData.pass));
                StringBuilder sourceString21 = new StringBuilder();
                sourceString21.Append(this.HPplus);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString21, SaveData.pass));
                StringBuilder sourceString22 = new StringBuilder();
                foreach (int num in this.keyitem)
                    sourceString22.Append(num.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString22, SaveData.pass));
                StringBuilder sourceString23 = new StringBuilder();
                foreach (int num in this.mail)
                    sourceString23.Append(num.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString23, SaveData.pass));
                StringBuilder sourceString24 = new StringBuilder();
                foreach (bool flag in this.mailread)
                    sourceString24.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString24, SaveData.pass));
                StringBuilder sourceString25 = new StringBuilder();
                sourceString25.Append(this.manybattle);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString25, SaveData.pass));
                StringBuilder sourceString26 = new StringBuilder();
                sourceString26.Append(this.MaxCore);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString26, SaveData.pass));
                StringBuilder sourceString27 = new StringBuilder();
                sourceString27.Append(this.MaxHz);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString27, SaveData.pass));
                StringBuilder sourceString28 = new StringBuilder();
                sourceString28.Append(this.mind);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString28, SaveData.pass));
                StringBuilder sourceString29 = new StringBuilder();
                sourceString29.Append(this.Money);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString29, SaveData.pass));
                StringBuilder sourceString30 = new StringBuilder();
                sourceString30.Append(this.plase);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString30, SaveData.pass));
                StringBuilder sourceString31 = new StringBuilder();
                sourceString31.Append(this.isJackedIn);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString31, SaveData.pass));
                StringBuilder sourceString32 = new StringBuilder();
                foreach (byte num in this.regularchip)
                    sourceString32.Append(((int)num).ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString32, SaveData.pass));
                StringBuilder sourceString33 = new StringBuilder();
                foreach (bool flag in this.regularflag)
                    sourceString33.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString33, SaveData.pass));
                StringBuilder sourceString34 = new StringBuilder();
                sourceString34.Append(this.Regularlarge);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString34, SaveData.pass));
                StringBuilder sourceString35 = new StringBuilder();
                foreach (bool runSubChip in this.runSubChips)
                    sourceString35.Append(runSubChip.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString35, SaveData.pass));
                StringBuilder sourceString36 = new StringBuilder();
                sourceString36.Append(this.selectQuestion);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString36, SaveData.pass));
                StringBuilder sourceString37 = new StringBuilder();
                sourceString37.Append(this.setstyle);
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString37, SaveData.pass));
                StringBuilder sourceString38 = new StringBuilder();
                foreach (Virus stockViru in this.stockVirus)
                    sourceString38.Append(stockViru.type.ToString() + "/" + stockViru.eatBug + "/" + stockViru.eatError + "/" + stockViru.eatFreeze + "/" + stockViru.code + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString38, SaveData.pass));
                StringBuilder sourceString39 = new StringBuilder();
                foreach (Style style in this.style)
                    sourceString39.Append(style.style.ToString() + "/" + style.element + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString39, SaveData.pass));
                StringBuilder sourceString40 = new StringBuilder();
                foreach (int num in this.stylepoint)
                    sourceString40.Append(num.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString40, SaveData.pass));
                StringBuilder sourceString41 = new StringBuilder();
                foreach (byte num in this.time)
                    sourceString41.Append(((int)num).ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString41, SaveData.pass));
                StringBuilder sourceString42 = new StringBuilder();
                foreach (bool flag in this.virusSPbusted)
                    sourceString42.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString42, SaveData.pass));
                StringBuilder sourceString43 = new StringBuilder();
                foreach (bool flag in this.virusSPbustedFlug)
                    sourceString43.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString43, SaveData.pass));
                StringBuilder sourceString44 = new StringBuilder();
                bool[,] bbsRead = this.bbsRead;
                int upperBound1 = bbsRead.GetUpperBound(0);
                int upperBound2 = bbsRead.GetUpperBound(1);
                for (int lowerBound1 = bbsRead.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
                {
                    for (int lowerBound2 = bbsRead.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
                    {
                        bool flag = bbsRead[lowerBound1, lowerBound2];
                        sourceString44.Append(flag.ToString() + "@");
                    }
                }
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString44, SaveData.pass));
                StringBuilder sourceString45 = new StringBuilder();
                foreach (bool flag in this.questEnd)
                    sourceString45.Append(flag.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString45, SaveData.pass));
                stringBuilder = new StringBuilder();
                this.shopThread.Join();
                streamWriter.WriteLine(this.shoplist);
                StringBuilder sourceString46 = new StringBuilder();
                sourceString46.Append(this.message.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString46, SaveData.pass));
                StringBuilder sourceString47 = new StringBuilder();
                sourceString47.Append(this.isJackedIn.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString47, SaveData.pass));
                StringBuilder sourceString48 = new StringBuilder();
                sourceString48.Append(this.nowMap.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString48, SaveData.pass));
                StringBuilder sourceString49 = new StringBuilder();
                sourceString49.Append(this.nowX.ToString(System.Globalization.CultureInfo.InvariantCulture));
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString49, SaveData.pass));
                StringBuilder sourceString50 = new StringBuilder();
                sourceString50.Append(this.nowY.ToString(System.Globalization.CultureInfo.InvariantCulture));
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString50, SaveData.pass));
                StringBuilder sourceString51 = new StringBuilder();
                sourceString51.Append(this.nowZ.ToString(System.Globalization.CultureInfo.InvariantCulture));
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString51, SaveData.pass));
                StringBuilder sourceString52 = new StringBuilder();
                sourceString52.Append(this.nowFroor.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString52, SaveData.pass));
                StringBuilder sourceString53 = new StringBuilder();
                sourceString53.Append(this.steptype.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString53, SaveData.pass));
                StringBuilder sourceString54 = new StringBuilder();
                sourceString54.Append(this.stepoverX.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString54, SaveData.pass));
                StringBuilder sourceString55 = new StringBuilder();
                sourceString55.Append(this.stepoverY.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString55, SaveData.pass));
                StringBuilder sourceString56 = new StringBuilder();
                sourceString56.Append(this.stepCounter.ToString(System.Globalization.CultureInfo.InvariantCulture));
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString56, SaveData.pass));
                StringBuilder sourceString57 = new StringBuilder();
                sourceString57.Append(this.pluginMap.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString57, SaveData.pass));
                StringBuilder sourceString58 = new StringBuilder();
                sourceString58.Append(this.pluginX.ToString(System.Globalization.CultureInfo.InvariantCulture));
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString58, SaveData.pass));
                StringBuilder sourceString59 = new StringBuilder();
                sourceString59.Append(this.pluginY.ToString(System.Globalization.CultureInfo.InvariantCulture));
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString59, SaveData.pass));
                StringBuilder sourceString60 = new StringBuilder();
                sourceString60.Append(this.pluginZ.ToString(System.Globalization.CultureInfo.InvariantCulture));
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString60, SaveData.pass));
                StringBuilder sourceString61 = new StringBuilder();
                sourceString61.Append(this.pluginFroor.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString61, SaveData.pass));
                this.flagThread.Join();
                streamWriter.WriteLine(this.fluglist);
                this.valThread.Join();
                streamWriter.WriteLine(this.vallist);
                this.mysThread.Join();
                streamWriter.WriteLine(this.myslist);
                this.ranThread.Join();
                streamWriter.WriteLine(this.ranlist);
                StringBuilder sourceString62 = new StringBuilder();
                for (int index = 0; index < this.interiors.Count; ++index)
                {
                    sourceString62.Append(this.interiors[index].number.ToString() + "@");
                    sourceString62.Append(this.interiors[index].posiX.ToString() + "@");
                    sourceString62.Append(this.interiors[index].posiY.ToString() + "@");
                    sourceString62.Append(this.interiors[index].set.ToString() + "@");
                    sourceString62.Append(this.interiors[index].rebirth.ToString() + "@");
                }
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString62, SaveData.pass));
                StringBuilder sourceString63 = new StringBuilder();
                foreach (int num in this.netWorkName)
                    sourceString63.Append(num.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString63, SaveData.pass));
                StringBuilder sourceString64 = new StringBuilder();
                sourceString64.Append(this.netWorkFace.ToString());
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString64, SaveData.pass));
                StringBuilder sourceString65 = new StringBuilder();
                foreach (int num in this.RirekNetWorkAddress)
                    sourceString65.Append(num.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString65, SaveData.pass));
                StringBuilder sourceString66 = new StringBuilder();
                foreach (int num in this.RirekNetWorkFace)
                    sourceString66.Append(num.ToString() + "@");
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString66, SaveData.pass));
                StringBuilder sourceString67 = new StringBuilder();
                foreach (int[] numArray in this.RirekNetWorkName)
                {
                    string str = "";
                    for (int index = 0; index < numArray.Length; ++index)
                        str = str + numArray[index] + ",";
                    sourceString67.Append(str + "@");
                }
                streamWriter.WriteLine(TCDEncodeDecode.EncryptString(sourceString67, SaveData.pass));
            }
            catch
            {
                saveFailed = true;
            }
            finally
            {
                streamWriter?.Close();
                streamWriter?.Dispose();
            }

            if (saveFailed)
            {
                var errorText = ShanghaiEXE.Translate("Save.SaveFailed").Text;
                if (File.Exists(BackupPath))
                {
                    File.Copy(BackupPath, SavePath, true);
                    errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.SaveRetained").Text;
                }
                else
                {
                    errorText += Environment.NewLine + Environment.NewLine + ShanghaiEXE.Translate("Save.SaveBackupNotFound").Text;
                }

                parent?.Invoke((Action)(() =>
                {
                    MessageBox.Show(
                        errorText,
                        ShanghaiEXE.Translate("Save.SaveFailedTitle").Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }));
            }
            else
            {
                if (File.Exists(SavePath))
                {
                    File.Copy(SavePath, BackupPath, true);
                }
                File.Copy(SavePathTemp, SavePath, true);
                File.Delete(SavePathTemp);
            }

            this.saveEnd = true;
            this.saveEndnowsub = true;
            this.saveEndnow = true;
        }

        public void FlugSave()
        {
            this.flugEnd = false;
            StringBuilder sourceString = new StringBuilder();
            foreach (bool flag in this.flagList)
                sourceString.Append(flag.ToString() + "@");
            this.fluglist = TCDEncodeDecode.EncryptString(sourceString, SaveData.pass);
            this.flugEnd = true;
        }

        public void ValSave()
        {
            this.valEnd = false;
            StringBuilder sourceString = new StringBuilder();
            foreach (int val in this.valList)
                sourceString.Append(val.ToString() + "@");
            this.vallist = TCDEncodeDecode.EncryptString(sourceString, SaveData.pass);
            this.valEnd = true;
        }

        public void ShopSave()
        {
            this.shopEnd = false;
            StringBuilder sourceString = new StringBuilder();
            for (int index1 = 0; index1 < this.shopCount.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < this.shopCount.GetLength(1); ++index2)
                {
                    sourceString.Append(this.shopCount[index1, index2].ToString());
                    sourceString.Append("/");
                }
            }
            this.shoplist = TCDEncodeDecode.EncryptString(sourceString, SaveData.pass);
            this.shopEnd = true;
        }

        public void MysSave()
        {
            this.mysEnd = false;
            StringBuilder sourceString = new StringBuilder();
            foreach (bool flag in this.getMystery)
                sourceString.Append(flag.ToString() + "@");
            this.myslist = TCDEncodeDecode.EncryptString(sourceString, SaveData.pass);
            this.mysEnd = true;
        }

        public void RanSave()
        {
            this.ranEnd = false;
            StringBuilder sourceString = new StringBuilder();
            foreach (bool flag in this.getRandomMystery)
                sourceString.Append(flag.ToString() + "@");
            this.ranlist = TCDEncodeDecode.EncryptString(sourceString, SaveData.pass);
            this.ranEnd = true;
        }

        public void ChipSave()
        {
            this.chipEnd = false;
            StringBuilder sourceString = new StringBuilder();
            for (int index1 = 0; index1 < this.havechip.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < this.havechip.GetLength(1); ++index2)
                    sourceString.Append(((int)this.havechip[index1, index2]).ToString() + "@");
                sourceString.Append("/");
            }
            this.chiplist = TCDEncodeDecode.EncryptString(sourceString, SaveData.pass);
            this.chipEnd = true;
        }

        public int HPmax
        {
            get
            {
                return this.hpmax;
            }
            set
            {
                this.hpmax = value;
                if (this.hpmax <= 1000)
                    return;
                this.hpmax = 1000;
            }
        }

        public int HPMax
        {
            get
            {
                if (this.HPmax + this.HPplus <= 0)
                    return 1;
                return this.HPmax + this.HPplus;
            }
        }

        public int HPNow
        {
            get
            {
                return this.HPnow;
            }
            set
            {
                this.HPnow = value;
                if (this.HPnow <= 0)
                    this.HPnow = 1;
                if (this.HPnow <= this.HPMax)
                    return;
                this.HPnow = this.HPMax;
            }
        }

        public byte Regularlarge
        {
            get
            {
                return this.regularlarge;
            }
            set
            {
                this.regularlarge = value;
                if (this.regularlarge <= 50)
                    return;
                this.regularlarge = 50;
            }
        }

        public byte[,] Havechip
        {
            get
            {
                return this.havechip;
            }
            set
            {
                this.havechip = value;
            }
        }

        public int NaviFolder
        {
            set
            {
                this.naviFolder = value;
            }
            get
            {
                if (this.naviFolder > 0)
                    return this.naviFolder;
                return 0;
            }
        }

        public int MaxHz
        {
            get
            {
                return this.maxhz;
            }
            set
            {
                this.maxhz = value;
                if (this.maxhz > 20)
                    this.maxhz = 20;
                if (this.maxhz >= 1)
                    return;
                this.maxhz = 1;
            }
        }

        public int MaxCore
        {
            get
            {
                return this.maxcore;
            }
            set
            {
                this.maxcore = value;
                if (this.maxcore > 5)
                    this.maxcore = 5;
                if (this.maxcore >= 1)
                    return;
                this.maxcore = 1;
            }
        }

        public int Money
        {
            get
            {
                return this.money;
            }
            set
            {
                long num = value;
                if (num > int.MaxValue)
                    num = int.MaxValue;
                this.money = (int)num;
            }
        }

        public Virus[] HaveVirus
        {
            get
            {
                return this.haveVirus;
            }
            set
            {
                this.haveVirus = value;
                SaveData.HAVEVirus = value;
            }
        }

        public void AddonSkillReset()
        {
            this.addonSkill = new bool[Enum.GetNames(typeof(SaveData.ADDONSKILL)).Length];
        }

        public int[,] ShopCount
        {
            get
            {
                return this.shopCount;
            }
            set
            {
                this.shopCount = value;
                this.shopThread = new Thread(new ThreadStart(this.ShopSave));
                this.shopThread.Start();
            }
        }

        public bool[] FlagList
        {
            get
            {
                return this.flagList;
            }
            set
            {
                this.flagList = value;
                this.flagThread = new Thread(new ThreadStart(this.FlugSave));
                this.flagThread.Start();
            }
        }

        public VariableArray ValList
        {
            get
            {
                return this.valList;
            }
            set
            {
                this.valList = value;
                this.valThread = new Thread(new ThreadStart(this.ValSave));
                this.valThread.Start();
            }
        }

        public bool[] GetMystery
        {
            get
            {
                return this.getMystery;
            }
            set
            {
                this.getMystery = value;
            }
        }

        public bool[] GetRandomMystery
        {
            get
            {
                return this.getRandomMystery;
            }
            set
            {
                this.getRandomMystery = value;
            }
        }

        public object AllItems { get; private set; }

        public static void PadNumChange(int PadNum, int Button, int Value)
        {
            SaveData.Pad[PadNum, Button] = Value;
        }

        public SaveData()
        {
            SaveData.pass = "evAJ5h1lGgmYm0EgZbbraA==";
        }

        public void Init()
        {
            //function that's called when you hit newgame normally, we're hijacking this in part to set
            //freeplay stuff

            for (int i = 0; i < 9999; i++)
            {
                apgive[i, 6] = -1;

            }

            ap_itemq = 0;
            this.manybattle = 0;
            this.HPmax = 200;
            this.HPnow = this.HPmax;
            for (int index = 0; index < this.style.Length; ++index)
                this.style[index] = new Style();
            this.style[0].style = 0;
            this.style[0].element = 0;
            this.setstyle = 0;
            this.havestyles = 1;
            this.efolder = 0;
            for (int index = 0; index < this.canselectmenu.Length; ++index)
                this.canselectmenu[index] = true;
            for (int index = 0; index < this.datelist.Length; ++index)
                this.datelist[index] = false;
            for (int index1 = 0; index1 < this.havechip.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < this.havechip.GetLength(1); ++index2)
                    this.havechip[index1, index2] = 0;
            }
            this.havechips.Clear();
            for (int index1 = 0; index1 < this.shopCount.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < this.shopCount.GetLength(1); ++index2)
                    this.shopCount[index1, index2] = 0;
            }
           // for (int index = 0; index < this.regularflag.Length; ++index)
           //     this.regularflag[index] = false;
            for (int index = 0; index < this.regularchip.Length; ++index)
                this.regularchip[index] = 0;
            this.HaveVirus = new Virus[3];
            this.stockVirus = new List<Virus>();
            for (int index = 0; index < this.runSubChips.Length; ++index)
                this.runSubChips[index] = false;
            this.interiors.Clear();
            //APTODO: this inits new save file crap
            this.Money = 1000; //default is 1k, probly up a bit
            this.MaxHz = 10;
            this.MaxCore = 2;
            this.NaviFolder = 5;
            this.darkFolder = 1;
            this.plusFolder = 0;
            this.haveAddon.Clear();
            this.equipAddon.Clear();
            for (int index = 0; index < this.busterspec.Length; ++index)
            {
                this.busterspec[index] = 0;
                this.busterspec[index] = 1;
            }
            for (int index1 = 0; index1 < this.chipFolder.GetLength(0); ++index1)
            {
                //Console.WriteLine(this.chipFolder.ToString());

                for (int index2 = 0; index2 < this.chipFolder.GetLength(1); ++index2)
                {
                    for (int index3 = 0; index3 < this.chipFolder.GetLength(2); ++index3)
                    {
                        //Console.WriteLine(this.chipFolder[index1,index2,index3].ToString());
                        this.chipFolder[index1, index2, index3] = 0;
                        if (index1 == 0)
                            this.chipFolder[index1, index2, index3] = index2 > 1 ? (index2 > 3 ? (index2 > 5 ? (index2 > 7 ? (index2 > 9 ? (index2 > 12 ? (index2 > 14 ? (index2 > 17 ? (index2 > 19 ? (index2 > 20 ? (index2 > 22 ? (index2 > 25 ? (index2 > 28 ? (index3 != 0 ? 0 : 190) : (index3 != 0 ? 2 : 188)) : (index3 != 0 ? 2 : 174)) : (index3 != 0 ? 1 : 158)) : (index3 != 0 ? 0 : 136)) : (index3 != 0 ? 1 : 121)) : (index3 != 0 ? 2 : 100)) : (index3 != 0 ? 1 : 62)) : (index3 != 0 ? 1 : 59)) : (index3 != 0 ? 0 : 59)) : (index3 != 0 ? 2 : 43)) : (index3 != 0 ? 0 : 43)) : (index3 != 0 ? 1 : 1)) : (index3 != 0 ? 0 : 1);
                        if (index1 == 1)
                            this.chipFolder[index1, index2, index3] = index2 > 1 ? (index2 > 3 ? (index2 > 5 ? (index2 > 7 ? (index2 > 10 ? (index2 > 13 ? (index2 > 15 ? (index2 > 17 ? (index2 > 21 ? (index2 > 25 ? (index2 > 28 ? (index3 != 0 ? 0 : 188) : (index3 != 0 ? 2 : 174)) : (index3 != 0 ? 1 : 158)) : (index3 != 0 ? 1 : 121)) : (index3 != 0 ? 2 : 43)) : (index3 != 0 ? 1 : 43)) : (index3 != 0 ? 2 : 100)) : (index3 != 0 ? 1 : 62)) : (index3 != 0 ? 2 : 59)) : (index3 != 0 ? 0 : 59)) : (index3 != 0 ? 1 : 1)) : (index3 != 0 ? 0 : 1);
                        if (index1 == 2)
                            this.chipFolder[index1, index2, index3] = index2 > 1 ? (index2 > 3 ? (index2 > 5 ? (index2 > 7 ? (index2 > 10 ? (index2 > 13 ? (index2 > 15 ? (index2 > 17 ? (index2 > 20 ? (index2 > 22 ? (index2 > 26 ? (index3 != 0 ? 0 : 188) : (index3 != 0 ? 2 : 174)) : (index3 != 0 ? 1 : 158)) : (index3 != 0 ? 1 : 121)) : (index3 != 0 ? 2 : 43)) : (index3 != 0 ? 1 : 43)) : (index3 != 0 ? 2 : 100)) : (index3 != 0 ? 1 : 62)) : (index3 != 0 ? 2 : 59)) : (index3 != 0 ? 0 : 59)) : (index3 != 0 ? 1 : 1)) : (index3 != 0 ? 0 : 1);
                    }
                }
            }


            for (int index = 0; index < this.stylepoint.Length; ++index)
                this.stylepoint[index] = 0;
            this.havefolder[1] = false; //maybe make extra folders checks?
            this.havefolder[2] = false;
            for (int index = 0; index < this.HaveVirus.Length; ++index)
                this.HaveVirus[index] = null;
            this.haveCaptureBomb = 0;
            for (int index = 0; index < this.havePeace.Length; ++index)
                this.havePeace[index] = 0;
            this.mind = 0;
            //for (int index = 0; index < this.flagList.Length; ++index)
            //    this.flagList[index] = false;
            for (int index = 0; index < this.valList.Count; ++index)
                this.valList[index] = 0;
            for (int index = 0; index < this.getMystery.Length; ++index)
                this.getMystery[index] = false;
            for (int index = 0; index < this.getRandomMystery.Length; ++index)
                this.getRandomMystery[index] = false;
            for (int index1 = 0; index1 < this.bbsRead.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < this.bbsRead.GetLength(1); ++index2)
                    this.bbsRead[index1, index2] = false;
            }
            for (int index = 0; index < this.questEnd.Length; ++index)
                this.questEnd[index] = false;
            for (int index = 0; index < this.virusSPbustedFlug.Length; ++index)
                this.virusSPbustedFlug[index] = false;
            for (int index = 0; index < this.haveSubChis.Length; ++index)
                this.haveSubChis[index] = 0;
            for (int index = 0; index < this.virusSPbusted.Length; ++index)
                this.virusSPbusted[index] = false;
            for (int index = 0; index < this.questEnd.Length; ++index)
                this.questEnd[index] = false;
            this.mailread.Clear();
            this.mail.Clear();
            this.keyitem.Clear();
            this.AddonReset();
            this.fukasinArea = 0;
            this.time = new byte[4];
            this.haveSubMemory = 2;
            this.havefolder[0] = true;
            this.Regularlarge = 4;
            this.datelist[0] = true;
            this.datelist[42] = true;
            this.datelist[58] = true;
            this.datelist[61] = true;
            this.datelist[99] = true;
            this.datelist[120] = true;
            this.datelist[135] = true;
            this.datelist[157] = true;
            this.datelist[173] = true;
            this.datelist[187] = true;
            this.datelist[189] = true;
            this.shopThread = new Thread(new ThreadStart(this.ShopSave));
            this.shopThread.Start();
            //this.flagThread = new Thread(new ThreadStart(this.FlugSave));
           // this.flagThread.Start();
            this.valThread = new Thread(new ThreadStart(this.ValSave));
            this.valThread.Start();
            this.mysThread = new Thread(new ThreadStart(this.MysSave));
            this.mysThread.Start();
            this.ranThread = new Thread(new ThreadStart(this.RanSave));
            this.ranThread.Start();
            this.chipThread = new Thread(new ThreadStart(this.ChipSave));
            this.chipThread.Start();
        }

        public void TimePlus()
        {

            ++this.time[0];
            if (this.time[0] < 60)
                return;
            this.time[0] = 0;
            ++this.time[1];
            if (this.time[1] >= 60)
            {
                this.time[1] = 0;
                ++this.time[2];
                if (this.time[2] >= 60)
                {
                    this.time[2] = 0;
                    ++this.time[3];
                }
            }
        }

        public string GetTime()
        {
            string str1 = "";
            if (this.time[3] < 10)
                str1 += "0";
            string str2 = str1 + this.time[3] + ":";
            if (this.time[2] < 10)
                str2 += "0";
            return str2 + this.time[2];
        }

        public string GetHaveManyChips()
        {
            var completionLibrary = new Library(null, null, null, this);
            var normalChips = completionLibrary.LibraryPages[LibraryPageType.Normal].Chips.Count(c => c.IsSeen);
            var naviChips = completionLibrary.LibraryPages[LibraryPageType.Navi].Chips.Count(c => c.IsSeen);
            var darkChips = completionLibrary.LibraryPages[LibraryPageType.Dark].Chips.Count(c => c.IsSeen);
            return $"S{normalChips}/ N{naviChips}/ D{darkChips}";
        }

        public string GetHaveChips()
        {
            int num1 = 0;
            foreach (bool flag in this.havefolder)
            {
                if (flag)
                    num1 += 30;
            }
            byte[,] havechip = this.havechip;
            int upperBound1 = havechip.GetUpperBound(0);
            int upperBound2 = havechip.GetUpperBound(1);
            for (int lowerBound1 = havechip.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
            {
                for (int lowerBound2 = havechip.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
                {
                    byte num2 = havechip[lowerBound1, lowerBound2];
                    num1 += num2;
                }
            }
            return num1.ToString();
        }

        public void AddOnRUN()
        {
            this.AddonReset();
            for (int index = 0; index < this.haveAddon.Count; ++index)
            {
                if (this.equipAddon[index])
                {
                    this.addonNames.Add(this.haveAddon[index].name);
                    this.haveAddon[index].Running(this);
                }
            }
            this.AddonSet();
        }

        public void GetAddon(AddOnBase addon)
        {
            this.haveAddon.Add(addon);
            this.equipAddon.Add(false);
        }

        public void AddonReset()
        {
            this.addonNames.Clear();
            for (int index = 0; index < this.busterspec.Length; ++index)
            {
                this.busterspec[index] = 0;
                this.busterspec[index] = 1;
            }
            this.addonSkill = new bool[Enum.GetNames(typeof(SaveData.ADDONSKILL)).Length];
            this.message = 0;
            this.HPplus = 0;
            this.custom = 0;
            this.NaviFolder = 5;
            this.darkFolder = 1;
            this.plusFolder = 0;
        }

        public void AddonSet()
        {
            if (!this.isJackedIn)
                this.HPNow = this.HPMax;
            for (int index = 0; index < this.busterspec.Length; ++index)
            {
                if (this.busterspec[index] > 5)
                    this.busterspec[index] = 5;
            }
        }

        public int CodeCheck(int chipno, int chipcode)
        {
            ChipFolder chipFolder = new ChipFolder(null);
            chipFolder.SettingChip(chipno);
            for (int index = 0; index < 4; ++index)
            {
                if (chipFolder.chip.code[chipcode] == chipFolder.chip.code[index])
                {
                    chipcode = index;
                    break;
                }
            }
            return chipcode;
        }

        public void AddChip(int chipno, int chipcode, bool head)
        {
            try
            {
                if (chipno <= 0)
                    return;
                chipcode = this.CodeCheck(chipno, chipcode);
                if (!this.datelist[chipno - 1])
                    this.datelist[chipno - 1] = true;
                if (this.havechip[chipno, chipcode] < 99)
                    ++this.havechip[chipno, chipcode];
                if (!head)
                {
                    if (this.ListCheck(chipno, chipcode))
                        return;
                    this.havechips.Add(new ChipS(chipno, chipcode));
                }
                else
                {
                    if (this.ListCheck(chipno, chipcode))
                    {
                        int index = this.ListCheckNumber(chipno, chipcode);
                        if (index != -1)
                            this.havechips.RemoveAt(index);
                    }
                    this.havechips.Insert(0, new ChipS(chipno, chipcode));
                }
            }
            catch
            {
            }
        }

        public void LosChip(int chipno, int chipcode)
        {
            if (this.havechip[chipno, chipcode] > 0)
                --this.havechip[chipno, chipcode];
            if (this.havechip[chipno, chipcode] != 0 || !this.ListCheck(chipno, chipcode))
                return;
            int index = this.ListCheckNumber(chipno, chipcode);
            if (index != -1)
                this.havechips.RemoveAt(index);
        }

        public bool ListCheck(int chipno, int chipcode)
        {
            foreach (ChipS havechip in this.havechips)
            {
                if (havechip.number == chipno && havechip.code == chipcode)
                    return true;
            }
            return false;
        }

        public int ListCheckNumber(int chipno, int chipcode)
        {
            for (int index = 0; index < this.havechips.Count; ++index)
            {
                if (this.havechips[index].number == chipno && this.havechips[index].code == chipcode)
                    return index;
            }
            return -1;
        }

        private static float FloatParseAnySeparator(string str)
        {
            var parseCulture = str.Contains(",")
                // known culture using comma as decimal
                ? System.Globalization.CultureInfo.GetCultureInfo("es-ES")
                : System.Globalization.CultureInfo.InvariantCulture;

            return float.Parse(str, parseCulture);
        }

        public enum EXMESSID
        {
            LHint1,
            LHint2,
            QHint1,
            QHint2,
            Omake1,
            Omake2,
            Omake3,
            Omake4,
            Qinfo,
            QinfoEnd,
            BBS1,
            BBS2,
            BBS3,
            BBS4,
            BBS5,
            BBS6,
        }

        public enum ADDONSKILL
        {
            // BltzBstr
            アサルトバスター,
            // RichRich
            確実ゼニー取得,
            // DataFind
            確実チップ取得,
            // (UNUSED? "Suppresses the appearance of weak enemies", Firewall?)
            弱い敵の出現抑制,
            // (UNUSED? "Charge Shot Invincibility", SprArmr?)
            チャージショット無敵,
            // (UNUSED? "Damage invincibility is doubled", Early DmgGhost?)
            ダメージ無敵時間２倍,
            // FullOpen
            最初のターンだけフルオープン,
            // HeatPeac
            炎属性エンカウント,
            // AquaPeac
            水属性エンカウント,
            // LeafPeac
            草属性エンカウント,
            // ElecPeac
            雷属性エンカウント,
            // PoisPeac
            毒属性エンカウント,
            // ErthPeac
            土属性エンカウント,
            // CAuraSrd
            Ｃオーラソード,
            // CDustBom
            Ｃダストボム,
            // CVulcan
            Ｃバルカン,
            // CFalKnif
            Ｃフォールナイフ,
            // CBlstCan
            Ｃブラストカノン,
            // CLance
            Ｃランス,
            // CShotGun
            Ｃショットガン,
            // CRecov
            Ｃリペア,
            // RStrShld
            Ｒボタンシールド,
            // RHoleFix
            Ｒボタン穴塞ぎ,
            // RPnkCrak
            Ｒボタン穴あけ,
            // LCube
            Ｌボタンキューブ,
            // LHeadWnd
            Ｌボタン向かい風,
            // LTailWnd
            Ｌボタン追い風,
            // LLockOn
            Ｌボタンロックオン,
            // Giv&Take
            ユズリアイ,
            // ReStyle
            スタイル再利用,
            // SpdRunner
            倍速移動,
            // ChipCure
            チップ使用回復,
            // (UNUSED? "Prevent push out", Head/Tailwind immunity?)
            押し出し防止,
            // (UNUSED? "Crack floor disabled", FlotShoe?)
            ヒビ床無効,
            // (UNUSED? "No folder shuffling", early UnShuffle?)
            フォルダシャッフル無し,
            // (UNUSED? "Zero version of Shinobi style", unknown)
            ゼロ版シノビスタイル,
            // Ammo
            薬莢,
            // BigAmmo
            薬莢大,
            // BlueBstr
            青バスター,
            // RunSoul
            逃走率１００パー,
            // MeltSelf
            常時メルト,
            // Slippery
            常時スリップ,
            // LostLght
            常時ブラインド,
            // HvyFoot
            常時ヘビィ,
            // AcidBody
            常時ポイズン,
            // CustPain
            カスタム毎にダメージ,
            // AreaHold
            フカシンエリア,
            // MyGarden
            自エリア整理,
            // SBarrier
            開始時バリア,
            // DmgGhost
            無敵時間増加,
            // HoldChrg
            チャージストック,
            // AngrMind
            不安が怒りに変化,
            // CrimNois
            イリーガルゲット,
            // ChipChrg
            チャージで威力１０アップ,
            // JunkBstr
            バスター空打ち,
            // DarkMind
            常にダーク状態,
            // StunDmg
            ダメージでマヒ,
            // HideLife
            敵HP視認不可,
            // LostArea
            ハイスイノジン,
            // (UNUSED? "Sense of Humor", early Humor?)
            ユーモアセンス,
            // (UNUSED? "Aelin Call", early EirnCall?)
            エーリンコール,
            // (UNUSED? "Line Change 1", unknown)
            セリフ変更１,
            // (UNUSED? "Line Change 2", unknown)
            セリフ変更２,
            // Statue
            フドウミョウオウ,
            // NoGuard
            ノーガード,
            // ChipPain
            ユーズドペイン,
            // SlowStrt
            スロウスタート,
            // HrdObjct
            ハードオブジェ,
            // AutoADD
            オートＡＤＤ,
            // AutoChrg
            オートチャージ,
            // UnderSht
            キシカイセイ,
            // Unshuffle
            アンシャッフル,
            // Scavenger
            Scavenger,
            // Sacrifice
            Sacrifice,
            // Mammon
            Mammon,
        }



        public void Maplootfinder()
        {

            for (int i = 0; i < 9999; i++)
            {
                apgive[i, 6] = -1;

            }

            var linenumb = 0;
            var mapno = 0;
            string txtname = "";
            var mapslookedthru = 0;
            var totalmystery = 0;
            var totalgifteditems = 0;
            var totalmaps = 175;
            var startofgifts = 700;
            

            int[,] sourcemap = new int[1000,2];
            int[] dupemode = new int[1000];

            string[,] giftitems = new string[1000, 3];

            string[] scramblegifts = new string[1000];


            //nowMap2 = txtname;
            var m = 0;
            for (m = 0; m < totalmaps + 1; m++)
            {
                txtname = FindMapName(mapno);

                //string text = "checking map: ";
                //Console.WriteLine($"{text}{txtname}");

                string path = Debug.MaskMapFile ? "MapData/" + txtname + ".shd" : "map/" + txtname + ".txt";
                if (!File.Exists(path))
                {
                   Console.WriteLine("Not found?");

                }
                else
                {
                    //Console.WriteLine("File found!");
                }



                StreamReader sr = new StreamReader(path);

                string searchtext = "Mystery:1,";
                string searchtext2 = "Mystery:2,";

                string gettext1 = "ItemGet:0";
                string gettext2 = "ItemGet:1";
                string gettext3 = "ItemGet:2";
                //string gettext4 = "ItemGet:3"; //this is shraed by key items and zenny, so zenny will have to remain untouched

                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    linenumb++;

                    if (line.Contains(searchtext) | line.Contains(searchtext2))
                    {
                        //Console.WriteLine(line);
                        string newstr = line;
                        string tempres = RemoveBeforeLastComma(newstr);
                        int fuckstrings = int.Parse(tempres);

                        int index = Array.IndexOf(dupemode,fuckstrings );

                        if (index == -1)
                        {

                            newstr = line.Replace(searchtext, "");
                            string mapstring = mapno.ToString();
                            //newstr = line.Replace(",,", mapstring); //unsure if i need this, come back later?
                            newstr = line.Replace(",,", "," + mapstring + ","); //unsure if i need this, come back later?
                                                                                //i defintely did need this, i'm a genius ^^^^
                                                                                //newstr = line.Replace(",,", ",-1,");

                            newstr = newstr.Replace("Mystery:", "");
                            /*
                            Console.Write("BMD: ");
                            Console.Write(newstr);

                            Console.WriteLine();
                            */
                            scrambleid[totalmystery] = newstr.ToString();
                            sourcemap[totalmystery, 0] = int.Parse(mapstring);

                            //string tempres = RemoveBeforeLastComma(newstr);
                            sourcemap[totalmystery, 1] = int.Parse(tempres);
                            dupemode[totalmystery] = int.Parse(tempres);


                            totalmystery++;
                        }
                        else
                        {
                            Console.WriteLine("Found a dupe, discarding: " + totalmystery);

                        }
                    }
                    else if (line.Contains(gettext1) | line.Contains(gettext2) | line.Contains(gettext3))
                    {
                        
                        string newstr = line;
                        newstr = newstr.Replace("ItemGet:", "");
                        newstr = newstr.Replace(":", ",");
                        int commacount = CountCommas(newstr);
                        if (newstr != "99,154,3"){ //fuck you blindleaf guy you're outa logic
                            if (commacount > 2) //filter out key items
                            {

                                string mapstring = mapno.ToString();

                                string newstry = newstr.Replace(",", "");


                                string tempopo = totalgifteditems.ToString();
                                //newstry += tempopo;

                                //for that one guy that gives you like 5 of the same chip
                                Console.WriteLine(newstry);
                                giftitems[totalgifteditems, 0] = newstry;
                                //Console.WriteLine(newstry);
                                //Console.WriteLine(txtname);
                                var tempvar = startofgifts + totalgifteditems;
                                var numba = tempvar.ToString();


                                string newstr2 = "1," + newstr + numba + "," + mapstring;
                                //Convert the gifted item output to a similar one to BMD's

                                
                                //flug starts at 700 instead of 0, so don't have 700 bmds i guess

                                giftitems[totalgifteditems, 1] = newstr2;
                                giftitems[totalgifteditems, 2] = mapstring;
                                giftitemsglobal[totalgifteditems] = newstry;
                                Console.WriteLine(newstry);

                                scramblegifts[totalgifteditems] = newstr2;
                                /*
                                Console.Write("Dag: ");
                                Console.Write(newstr2);
                                Console.WriteLine();
                                */
                        totalgifteditems++;

                            }
                        }

                    }
                }
                linenumb = 0;
                mapno++;
            }
            //totalmystery
            string txtr = "total BMD found: ";
            Console.WriteLine($"{txtr}{totalmystery}");

            //txtr = "total gifted items found: ";
            //Console.WriteLine($"{txtr}{totalgifteditems}");

            string teststr = "";


            
            int rng = ShanghaiEXE.Config.Seed;
            //rng = 0;

            Console.WriteLine("---Seed: " + rng + "---");
            //var (shuffled1, shuffled2) = ShuffleArrays(scrambleid, scramblegifts, rng);



            //scrambleid = shuffled1;
            //scramblegifts = shuffled2;

            //PrintArray(totalmystery);


            var shufflesize = totalgifteditems + totalmystery;

            //Console.Write("Size of both: ");
            //Console.Write(shufflesize);
            //Console.WriteLine();

            string[] combined = scrambleid.Concat(scramblegifts).ToArray();
            string[] cleanedcombined = RemoveBlankEntries(combined);
            //rng.Shuffle(combined);

            Random rng2 = new Random(rng);

            string[] cleanedcombined2 = cleanedcombined;

            if (rng > 0)
            {
                Shuffle(cleanedcombined, rng2);
                cleanedcombined2 = ShuffleArray(cleanedcombined, rng);
                Shuffle(cleanedcombined2, rng2);
            }


            for (int i = 0;i< totalmystery;i++)
            {
                scrambleid[i] = cleanedcombined2[i];


            }

            var spaceno = 0;

            for (int i = totalmystery; i < totalmystery+totalgifteditems; i++)
            {

                scramblegifts2[spaceno] = cleanedcombined2[i];
                //nsole.WriteLine(scramblegifts2[i]);
                spaceno++;

            }
            

            for (int i = 0; i < totalmystery; i++)
            {
                teststr = scrambleid[i];
                teststr = teststr.ToString();
                //teststr = Int32.Parse(teststr);

                string[] another = teststr.Split(',');
                scrambleid2[i, 0] = another[5]; //flag (flug lmao)
                scrambleid2[i, 1] = another[0]; //pickup type (1 = bmd, 2 = pmd, 0 is green so we don't care)
                scrambleid2[i, 2] = another[1]; //type (0 = chip, 1 = sub, 2 = addon, 3 = other)
                scrambleid2[i, 3] = another[2]; //number (chip number usually)
                scrambleid2[i, 4] = another[3]; //misc (chip code usually)
                scrambleid2[i, 5] = another[4]; //what map it's in


            }


            //print non scrambled BMD's
           

            ///


            int nNumber = 0;

            int listpoz = 0;

            for (int i = 0; i < 1000; i++)
            {
                Randolist2[i, 0] = -99;
                scrambleidfinal[i, 0] = -99;
                scrambleidfinal[i, 1] = -99;
                scrambleidfinal[i, 2] = -99;
            }
            


            //makes everything work?
            for (int i = 0; i < totalmystery; i++)
            {
                //Console.Write(listpoz + ": ");
                for (int j = 0; j < Randolist2.GetLength(1); j++)
                {
                    //scrambleidfinal[i, j] = int.TryParse(scrambleid2[i, j],out nNumber) ? nNumber : -1;

                    //scrambleidfinal[i, j] = 1;


                    Randolist2[listpoz, j] = int.TryParse(scrambleid2[i, j], out nNumber) ? nNumber : -1;

                    //Console.Write(Randolist2[listpoz, j] + " ");
                }
                listpoz++;
                //Console.WriteLine(); // Print a newline after each row
            }



            int[,] newsize = new int[listpoz, 6];

            


            int entno = 0;
            var entriesadded = 0;
            for (int i = 0; i < 999; i++)
            {
                if (Randolist2[i,0] != -99)
                    {
                    newsize[entno, 0] = Randolist2[i, 0];
                    newsize[entno, 1] = Randolist2[i, 1];
                    newsize[entno, 2] = Randolist2[i, 2];
                    newsize[entno, 3] = Randolist2[i, 3];
                    newsize[entno, 4] = Randolist2[i, 4];
                    newsize[entno, 5] = Randolist2[i, 5];

                    entno++;
                    
                }

            }
            //Console.WriteLine(entriesadded);

            //Conssole.WriteLine("----unshuffled-----");
            //PrintArray(newsize);

           // int rng = ShanghaiEXE.Config.Seed; //get RNG seed
            //rng = 0;

            //Random random = new Random(rng);  // Create a Random object with the provided seed

            
            /*
             * 
            if (rng > 0)
            {
                //int seed = 123456;
                int seed = rng;
                
                ShuffleRows(newsize, seed); //shuffle it up good
                ShuffleFirstColumn(newsize, seed); //then shuffle only the first column so that we can refer to it later
            }
            */
            //Console.WriteLine("----shuffled-----");
            //PrintArray(newsize);

            for (int i=0;i<999;i++)
            {
                scrambleidfinal[i, 0] = -99;

            }

            ShuffleFirstColumn(newsize, rng);

            int[,] newsize2 = new int[listpoz, 6];

            TextWriter originalConsoleOut = Console.Out;

            

            string filePath = "spoilerlog.txt";

            entriesadded = 0;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                Console.SetOut(writer); //actually sets output to spoiler log, comment out for debugging

                Console.WriteLine("----Seed: " + rng + "----");

                Console.WriteLine("----BMD----");
                //PrintArray(scrambleidfinal);
                entno = 0;
                int[] indexyoufuck = new int[999];
                
                /* old behaviour, index before printing (was innacurate and duplicated sometimes)

                for (int i = 0; i < totalmystery; i++)
                {
                    //ShuffleRows(newsize, seed);
                    var place = newsize[i, 0];
                    
                    //Console.WriteLine(place);

                    indexyoufuck[i] = place;

                    var resulter = FindIndex(indexyoufuck, place);

                    if (resulter != -1 && scrambleidfinal[i, 0] == -99)
                    {

                        scrambleidfinal[place, 0] = newsize[i, 0];
                        scrambleidfinal[place, 1] = newsize[i, 1];
                        scrambleidfinal[place, 2] = newsize[i, 2];
                        scrambleidfinal[place, 3] = newsize[i, 3];
                        scrambleidfinal[place, 4] = newsize[i, 4];
                        scrambleidfinal[place, 5] = newsize[i, 5];

                        //Console.WriteLine(place);

                        indexyoufuck[i] = place;
                        entriesadded++;
                    }
                    else
                    {
                        //Console.WriteLine("already indexed: " + place);



                    }

                    
                }

                */
                //Console.WriteLine("Scrambles found: " + entriesadded);

                entriesadded = 0;
                for (int i=0;i<999;i++)
                {
                    if (scrambleidfinal[i, 0] != -99)
                    {
                        //Console.WriteLine(scrambleidfinal[i, 0]);
                        entriesadded++;
                    }


                }
                //Console.WriteLine("Scrambles indexed: " + entriesadded);



                entriesadded = 0;
                //PrintArray(scrambleidfinal);

                //Console.WriteLine("----final scrambled list-----");
                //PrintArray(scrambleidfinal);



                int rowCount = sourcemap.GetLength(0);

                for (int i = 0; i < newsize.GetLength(0); i++)
                {

                    if (newsize[i, 0] > -1)
                    {
                        entriesadded++;
                        Console.Write(entno.ToString() + ": ");

                        entno++;
                        int typ = newsize[i, 2];
                        int entry = newsize[i, 3];
                        int entry2 = newsize[i, 4];

                        switch (typ)
                        {
                            case 0:
                                Console.Write("Chip : ");
                                ChipFolder chipFolder = new ChipFolder(null);
                                chipFolder.SettingChip(entry + 1);
                                string name = chipFolder.chip.name;
                                //TODO: figgure out how to get code later
                                Console.Write(name + " ");

                                var temp1 = entry + 1;
                                string stupei = temp1.ToString();
                                string junpei = entry2.ToString();

                                //int code2 = CodeCheck(temp1, entry2);
                                //Console.Write(code2 + " ");
                                Console.Write(entry2);

                                break;
                            case 1:
                                Console.Write("Sub chip: ");

                                switch (entry)
                                {
                                    case 0:
                                        Console.Write("Half Energy");
                                        break;
                                    case 1:
                                        Console.Write("Full Energy");
                                        break;
                                    case 2:
                                        Console.Write("Firewall");
                                        break;
                                    case 3:
                                        Console.Write("Open Port");
                                        break;
                                    case 4:
                                        Console.Write("Virus Scan");
                                        break;
                                    case 5:
                                        Console.Write("Crack Tool");
                                        break;


                                }

                                break;
                            case 2:
                                Console.Write("Addon: ");

                                string fuckwit = Addonnamegetter(entry);
                                Console.Write(fuckwit + "  ");
                                Console.Write(entry2);

                                break;
                            case 3:
                                Console.Write("Misc: ");
                                switch (entry)
                                {
                                    case 0:
                                        Console.Write("HP memory");
                                        break;
                                    case 1:
                                        Console.Write("Regup " + entry2);
                                        break;
                                    case 2:
                                        Console.Write("Sub Memory");
                                        break;
                                    case 3:
                                        Console.Write("Core Plus");
                                        break;
                                    case 4:
                                        Console.Write("Hertz Up " + entry2);
                                        break;
                                    case 5:
                                        Console.Write("Bug Frag " + entry2);
                                        break;
                                    case 6:
                                        Console.Write("Frz Frag " + entry2);
                                        break;
                                    case 7:
                                        Console.Write("Err Frag " + entry2);
                                        break;
                                    default:
                                        Console.Write("Zenny " + entry2);
                                        break;


                                }

                                break;


                        }





                        int mapo = newsize[i, 5];


                        var spot = FindInSecondColumn(sourcemap, newsize[i, 0]);

                        string map = "";

                        try
                        {
                            map = FindMapName(sourcemap[spot, 0]) + " " + newsize[i, 0];
                            
                        }
                        catch
                        {
                            var place = 700 - i;

                            place = Math.Abs(place);


                            map = "malformed map - " + place.ToString();

                        }
                        Console.Write(" " + map);

                        Console.WriteLine();

                        scrambleidfinal[newsize[i, 0], 0] = newsize[i, 0];
                        scrambleidfinal[newsize[i, 0], 1] = newsize[i, 1];
                        scrambleidfinal[newsize[i, 0], 2] = newsize[i, 2];
                        scrambleidfinal[newsize[i, 0], 3] = newsize[i, 3];
                        scrambleidfinal[newsize[i, 0], 4] = newsize[i, 4];
                        scrambleidfinal[newsize[i, 0], 5] = newsize[i, 5];
                        //scrambleidfinal[newsize[i, 0], 6] = newsize[i, 6];

                    }
                    //*/


                }



                entno = 0;
                Console.WriteLine("----Quests / Dialogue ----");

                for (int i =0; i < totalgifteditems;i++)
                {
                    Console.Write(i.ToString() + ": ");

                    //Console.WriteLine(scramblegifts2[i]);
                    string splungo = scramblegifts2[i];
                    
                    //Console.WriteLine(scramblegifts[i]);
                    string[] another = splungo.Split(',');

                    int typ = int.Parse(another[1]);
                    int entry = int.Parse(another[2]);
                    int entry2 = int.Parse(another[3]);


                    switch (typ)
                    {
                        case 0:
                            Console.Write("Chip : ");
                            ChipFolder chipFolder = new ChipFolder(null);
                            chipFolder.SettingChip(entry + 1);
                            string name = chipFolder.chip.name;
                            //TODO: figgure out how to get code later
                            Console.Write(name + " ");

                            var temp1 = entry + 1;
                            string stupei = temp1.ToString();
                            string junpei = entry2.ToString();

                            Console.Write(entry2);

                            //int code2 = CodeCheck(temp1, entry2);
                            //Console.Write(code2 + " ");


                            break;
                        case 1:
                            Console.Write("Subch: ");

                            switch (entry)
                            {
                                case 0:
                                    Console.Write("Half Energy");
                                    break;
                                case 1:
                                    Console.Write("Full Energy");
                                    break;
                                case 2:
                                    Console.Write("Firewall");
                                    break;
                                case 3:
                                    Console.Write("Open Port");
                                    break;
                                case 4:
                                    Console.Write("Virus Scan");
                                    break;
                                case 5:
                                    Console.Write("Crack Tool");
                                    break;


                            }

                            //switch (entry)
                                

                            break;
                        case 2:
                            Console.Write("Addon: ");

                            string fuckwit = Addonnamegetter(entry);
                            Console.Write(fuckwit + " ");
                            break;
                        case 3:
                            Console.Write("Misc: ");
                            switch (entry)
                            {
                                case 0:
                                    Console.Write("HP memory");
                                    break;
                                case 1:
                                    Console.Write("Regup " + entry2);
                                    break;
                                case 2:
                                    Console.Write("Sub Memory");
                                    break;
                                case 3:
                                    Console.Write("Core Plus");
                                    break;
                                case 4:
                                    Console.Write("Hertz Up " + entry2);
                                    break;
                                case 5:
                                    Console.Write("Bug Frag " + entry2);
                                    break;
                                case 6:
                                    Console.Write("Frz Frag " + entry2);
                                    break;
                                case 7:
                                    Console.Write("Err Frag " + entry2);
                                    break;
                                default:
                                    Console.Write("Zenny " + entry2);
                                    break;


                            }

                            break;
                        default:
                            Console.Write("Malformed type entry? : ");


                            break;

                    }

                    //scramblegifts[i]
                    string mapapapapo = giftitems[i, 2];
                    int mapopo2 = int.Parse(mapapapapo);

                    string map = FindMapName(mapopo2) + " " + i.ToString();

                    Console.Write(" " + map);

                    Console.WriteLine();
                    entno++;
                }


            }
            Console.SetOut(originalConsoleOut);
            //PrintArray(scrambleidfinal);
            Console.WriteLine("Spoiler log generated");

            
            listpoz = 0;


   



        }


        public void AP_Connect()
        {
            //Console.WriteLine(ShanghaiEXE.Config.Username);

            var session = ArchipelagoSessionFactory.CreateSession("archipelago.gg", 42391);
            Console.WriteLine(session);
            LoginResult result;

            //ArchipelagoSession
            try
            {
                result = session.TryConnectAndLogin("Shanghai.Exe", ShanghaiEXE.Config.Username, Archipelago.MultiClient.Net.Enums.ItemsHandlingFlags.AllItems);
            }
            catch(Exception e)
            {
                result = new LoginFailure(e.GetBaseException().Message);
            }


            if (!result.Successful)
            {
                LoginFailure failure = (LoginFailure)result;
                string errorMessage = $"Failed to Connect";
                foreach (string error in failure.Errors)
                {
                    errorMessage += $"\n    {error}";
                }
                foreach (Archipelago.MultiClient.Net.Enums.ConnectionRefusedError error in failure.ErrorCodes)
                {
                    errorMessage += $"\n    {error}";
                }

                MessageBox.Show(
                            errorMessage,
                            "AP error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                return; // Did not connect, show the user the contents of `errorMessage`
            }

            var loginSuccess = (LoginSuccessful)result;

        }

        

        private static void Connect(string server, string user, string pass)
        {

        }



        #region bad shuffling logic zone

        static void ShuffleRows(int[,] array, int seed)
        {
            Random rand = new Random(seed);

            // Get the number of rows
            int rowCount = array.GetLength(0);

            // Create an array of row indices
            int[] rowIndices = new int[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                rowIndices[i] = i;
            }

            // Shuffle the row indices array
            for (int i = rowCount - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);  // Get a random index
                                              // Swap elements
                int temp = rowIndices[i];
                rowIndices[i] = rowIndices[j];
                rowIndices[j] = temp;
            }

            // Create a temporary array to store scrambled rows
            int[,] scrambledArray = new int[rowCount, array.GetLength(1)];

            // Rearrange rows based on shuffled indices
            for (int i = 0; i < rowCount; i++)
            {
                int originalRowIndex = rowIndices[i];
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    scrambledArray[i, j] = array[originalRowIndex, j];
                }
            }

            // Copy the scrambled array back to the original array
            Array.Copy(scrambledArray, array, array.Length);
        }

        static void ShuffleFirstColumn(int[,] array, int seed)
        {
            Random rand = new Random(seed);
            int rows = array.GetLength(0);

            // Shuffle the first column
            for (int i = 0; i < rows; i++)
            {
                // Generate a random index to swap with
                int randomIndex = rand.Next(i, rows);

                // Swap the current element with the random index element in the first column
                int temp = array[i, 0];
                array[i, 0] = array[randomIndex, 0];
                array[randomIndex, 0] = temp;
            }
        }

        static void PrintArray(int[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static void PrintArrayStr(string[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static string FindMapName(int mapno)
        {
            string txtname = "missing!";
            switch(mapno)
                {
                    case 0:
                        txtname = "airCleaner1";
                break;
                    case 1:
                        txtname = "airCleaner2";
                break;
                    case 2:
                        txtname = "airCleaner3";
                break;
                    case 3:
                        txtname = "airCleaner4";
                break;
                    case 4:
                        txtname = "ariceTV";
                break;
                    case 5:
                        txtname = "battleship";
                break;
                    case 6:
                        txtname = "bench";
                break;
                    case 7:
                        txtname = "blackboard";
                break;
                    case 8:
                        txtname = "BronzeStatue";
                break;
                    case 9:
                        txtname = "BlackBoard";
                break;
                    case 10:
                        txtname = "blackbord";
                break;
                    case 11:
                        txtname = "cakeShop";
                break;
                    case 12:
                        txtname = "car";
                break;
                    case 13:
                        txtname = "CenterccityWest";
                break;
                    case 14:
                        txtname = "centerccityNorth";
                break;
                    case 15:
                        txtname = "centerccitySouth";
                break;
                    case 16:
                        txtname = "ChangeMachine";
                break;
                    case 17:
                        txtname = "cityNet1";
                break;
                    case 18:
                        txtname = "cityNet2";
                break;
                    case 19:
                        txtname = "cityNet3";
                break;
                    case 20:
                        txtname = "class2nen1kumi";
                break;
                    case 21:
                        txtname = "class2nen2kumi";
                break;
                    case 22:
                        txtname = "classPC";
                break;
                    case 23:
                        txtname = "clockTower1";
                break;
                    case 24:
                        txtname = "clockTower2";
                break;
                    case 25:
                        txtname = "clockTower3";
                break;
                    case 26:
                        txtname = "clockTower4";
                break;
                    case 27:
                        txtname = "clockTower5";
                break;
                    case 28:
                        txtname = "clockTowerReal1f";
                break;
                    case 29:
                        txtname = "clockTowerReal2f";
                break;
                    case 30:
                        txtname = "clockTowerReal3f";
                break;
                    case 31:
                        txtname = "clockTowerReal4f";
                break;
                    case 32:
                        txtname = "clockTowerReal5f";
                break;
                    case 33:
                        txtname = "Company3";
                break;
                    case 34:
                        txtname = "CompanyServer1";
                break;
                    case 35:
                        txtname = "CompanyServer2";
                break;
                    case 36:
                        txtname = "CompanyServer3";
                break;
                    case 37:
                        txtname = "CompanyServer4";
                break;
                    case 38:
                        txtname = "CompanyServerB";
                break;
                    case 39:
                        txtname = "Cruiser1f";
                break;
                    case 40:
                        txtname = "Cruiser1g";
                break;
                    case 41:
                        txtname = "Cruiser2f";
                break;
                    case 42:
                        txtname = "Cruiser2g";
                break;
                    case 43:
                        txtname = "Cruiser3f";
                break;
                    case 44:
                        txtname = "Cruiser3g";
                break;
                    case 45:
                        txtname = "cycle";
                break;
                    //ignore debug room and deepura
                    case 46:
                        txtname = "eienHighSchoolHP";
                break;
                    case 47:
                        txtname = "eienNet1";
                break;
                    case 48:
                        txtname = "eienNet2";
                break;
                    case 49:
                        txtname = "eienNet3";
                break;
                    case 50:
                        txtname = "eienSquare";
                break;
                    case 51:
                        txtname = "EienTown1";
                break;
                    case 52:
                        txtname = "EienTown2";
                break;
                    case 53:
                        txtname = "firehydrant";
                break;
                    case 54:
                        txtname = "flowerbed";
                break;
                    case 55:
                        txtname = "fountain";
                break;
                    case 56:
                        txtname = "gameCenter";
                break;
                    case 57:
                        txtname = "gameCenterCorridor";
                break;
                    case 58:
                        txtname = "gameCenterRoom";
                break;
                    case 59:
                        txtname = "gencity";
                break;
                    case 60:
                        txtname = "genNet1";
                break;
                    case 61:
                        txtname = "genNet2";
                break;
                    case 62:
                        txtname = "genNetSquare";
                break;
                    case 63:
                        txtname = "genschool1f";
                break;
                    case 64:
                        txtname = "genschool2fa";
                break;
                    case 65:
                        txtname = "genschool2fb";
                break;
                    case 66:
                        txtname = "cityNetSquare";
                break;
                    case 67:
                        txtname = "genUniversity";
                break;
                    case 68:
                        txtname = "genUniversity1f";
                break;
                    case 69:
                        txtname = "genUniversity2f";
                break;
                    case 70:
                        txtname = "genUniversity2fjim";
                break;
                    case 71:
                        txtname = "genUniversity2frin";
                break;
                    case 72:
                        txtname = "genUniversity3";
                break;
                    case 73:
                        txtname = "genUniversity3room";
                break;
                    case 74:
                        txtname = "HakureiJinja";
                break;
                    //heaven moved later
                    case 75:
                        txtname = "hospital1f";
                break;
                    case 76:
                        txtname = "hospital2f";
                break;
                    case 77:
                        txtname = "hospital2fRoom";
                break;
                    case 78:
                        txtname = "hospital3f";
                break;
                    case 79:
                        txtname = "HospitalNet1";
                break;
                    case 80:
                        txtname = "HospitalNet2";
                break;
                    case 81:
                        txtname = "HosTV";
                break;
                    case 82:
                        txtname = "hotelHP";
                break;
                    case 83:
                        txtname = "hotelRef";
                break;
                    case 84:
                        txtname = "ingleasHotel";
                break;
                    case 85:
                        txtname = "ingleasHouse";
                break;
                    case 86:
                        txtname = "ingleasTowerPark";
                break;
                    case 87:
                        txtname = "ingleasTown";
                break;
                    case 88:
                        txtname = "inglesNet1f";
                break;
                    case 89:
                        txtname = "inglesNet2f";
                break;
                    case 90:
                        txtname = "inglesNet3";
                break;
                    case 91:
                        txtname = "inglesSquare";
                break;
                    case 92:
                        txtname = "dami";
                break;
                    case 93:
                        txtname = "jinja1";
                break;
                    case 94:
                        txtname = "jinja2";
                break;
                    case 95:
                        txtname = "jinja3";
                break;
                    case 96:
                        txtname = "kumamiTank";
                break;
                    case 97:
                        txtname = "library";
                break;
                    case 98:
                        txtname = "lostShip1";
                break;
                    case 99:
                        txtname = "lostShip2";
                break;
                    case 100:
                        txtname = "mariHP";
                break;
                    case 101:
                        txtname = "mariroom";
                break;
                    case 102:
                        txtname = "neckHP";
                break;
                    case 103:
                        txtname = "neckRoom";
                break;
                    case 104:
                        txtname = "nekodolphin";
                break;
                    case 105:
                        txtname = "NetAgentSenter1f";
                break;
                    case 106:
                        txtname = "NetAgentSenter2f";
                break;
                    case 107:
                        txtname = "netBattleMachineGame";
                break;
                    case 108:
                        txtname = "dami";
                break;
                    case 109:
                        txtname = "netBattleMachineRin";
                break;
                    case 110:
                        txtname = "partytable";
                break;
                    case 111:
                        txtname = "Phone";
                break;
                    case 112:
                        txtname = "photoFrame";
                break;
                    case 113:
                        txtname = "piano";
                break;
                    case 114:
                        txtname = "post";
                break;
                    case 115:
                        txtname = "refrigerator1";
                break;
                    case 116:
                        txtname = "refrigerator2";
                break;
                    case 117:
                        txtname = "remihouse";
                break;
                    case 118:
                        txtname = "remiHP";
                break;
                    case 119:
                        txtname = "rikaHouse";
                break;
                    case 120:
                        txtname = "rikaHP";
                break;
                    case 121:
                        txtname = "ROMbase1f";
                break;
                    case 122:
                        txtname = "ROMbase2f";
                break;
                    case 123:
                        txtname = "ROMbase3f";
                break;
                    case 124:
                        txtname = "ROMbase4f";
                break;
                    case 125:
                        txtname = "ROMbase5f";
                break;
                    case 126:
                        txtname = "ROMbase6f";
                break;
                    case 127:
                        txtname = "ROMbase6f";
                break;
                    case 128:
                        txtname = "romDisplay";
                break;
                    case 129:
                        txtname = "ROMbaseOut";
                break;
                    case 130:
                        txtname = "ROMbaseOut";
                break;
                    case 131:
                        txtname = "romGate";
                break;
                    case 132:
                        txtname = "romGate";
                break;
                    case 133:
                        txtname = "ROMnet1";
                break;
                    case 134:
                        txtname = "ROMnet2";
                break;
                    case 135:
                        txtname = "ROMnet3";
                break;
                    case 136:
                        txtname = "ROMnet4";
                break;
                    case 137:
                        txtname = "SeirenShip1f";
                break;
                    case 138:
                        txtname = "SeirenShip1fbridge";
                break;
                    case 139:
                        txtname = "SeirenShip1fParty";
                break;
                    case 140:
                        txtname = "SeirenShip2f";
                break;
                    case 141:
                        txtname = "SeirenShipRoom103";
                break;
                    case 142:
                        txtname = "SeirenShip3f";
                break;
                    case 143:
                        txtname = "SeirenShipTV";
                break;
                    case 144:
                        txtname = "shoolNet1";
                break;
                    case 145:
                        txtname = "shoolNet1";
                break;
                    case 146:
                        txtname = "shoolNet2";
                break;
                    case 147:
                        txtname = "shoolNet3";
                break;
                    case 148:
                        txtname = "shoolNet4";
                break;
                    case 149:
                        txtname = "shoolNet4";
                break;
                    case 150:
                        txtname = "SNSkanri";
                break;
                    case 151:
                        txtname = "StaffRoom";
                break;
                    case 152:
                        txtname = "tenkoRoom";
                break;
                    case 153:
                        txtname = "tesuri";
                break;
                    case 154:
                        txtname = "UnderCable";
                break;
                    case 155:
                        txtname = "UnderGyosyou";
                break;
                    case 156:
                        txtname = "UnderSaikutu";
                break;
                    case 157:
                        txtname = "uraNet1";
                break;
                    case 158:
                        txtname = "uraNet2";
                break;
                    case 159:
                        txtname = "uraNet3";
                break;
                    case 160:
                        txtname = "uraNet4";
                break;
                    case 161:
                        txtname = "uraNet5";
                break;
                    case 162:
                        txtname = "uraNet6";
                break;
                    case 163:
                        txtname = "uraNet7";
                break;
                    case 164:
                        txtname = "uraNet8";
                break;
                    case 165:
                        txtname = "uraNet9";
                break;
                    case 166:
                        txtname = "uraNet10";
                break;
                    case 167:
                        txtname = "uraNetSquare";
                break;
                    case 168:
                        txtname = "uraNetSquare2";
                break;
                    case 169:
                        txtname = "usakou";
                break;
                    case 170:
                        txtname = "vendingMachine";
                break;
                    case 171:
                        txtname = "WiiU";
                break;
                    case 172:
                        txtname = "Yamada";
                break;
                    case 173:
                        txtname = "yukkuri";
                break;
                    case 174:
                        txtname = "heavenNet1";
                break;
                    case 175:
                        txtname = "heavenNet2";
                break;
                    default:
                    txtname = "???";
                break;
            }

            return txtname;

        }


        static string Addonnamegetter(int addonnum)
        {
            string txt = "???";

            switch (addonnum)
            {
                case 0: txt = "BustorPower "; break;
                case 1: txt = "BustorRapid "; break;
                case 2: txt = "BustorCharge "; break;
                case 3: txt = "AssaultBuster "; break;
                case 4: txt = "BlueBuster "; break;
                case 5: txt = "ChageBypass "; break;
                case 6: txt = "BustorSet "; break;
                case 8: txt = "HPPlus50 "; break;
                case 9: txt = "HPPlus100 "; break;
                case 10: txt = "HPPlus200 "; break;
                case 11: txt = "HPPlus500 "; break;
                case 12: txt = "RichRich "; break;
                case 13: txt = "DataSalvage "; break;
                case 14: txt = "HPPlus300 "; break;
                case 16: txt = "StockCharge "; break;
                case 17: txt = "DamageGhost "; break;
                case 18: txt = "FirstAdd "; break;
                case 19: txt = "FirstBarrier "; break;
                case 20: txt = "OneFullOpen "; break;
                case 21: txt = "AutoADD "; break;
                case 22: txt = "AutoCharge "; break;
                case 23: txt = "PeaceHeat "; break;
                case 24: txt = "PeaceAqua "; break;
                case 25: txt = "PeaceLeaf "; break;
                case 26: txt = "PeaceEleki "; break;
                case 27: txt = "PeacePoison "; break;
                case 28: txt = "PeaceEarth "; break;
                case 29: txt = "EriaGuard "; break;
                case 30: txt = "MyGarden "; break;
                case 31: txt = "Yuzuriai "; break;
                case 32: txt = "StyleReUse "; break;
                case 33: txt = "Haisui "; break;
                case 34: txt = "ChipSizeMinus "; break;
                case 35: txt = "ChipSizePlus "; break;
                case 36: txt = "NaviPlus "; break;
                case 37: txt = "DarkPlus "; break;
                case 38: txt = "UsedCure "; break;
                case 39: txt = "UsedPain "; break;
                case 40: txt = "Grimoire "; break;
                case 41: txt = "EscapeSoul "; break;
                case 42: txt = "UnShuffle "; break;
                case 43: txt = "AngerMind "; break;
                case 44: txt = "HardObject "; break;
                case 47: txt = "BuraiStyle "; break;
                case 48: txt = "Crimson Noise "; break;
                case 49: txt = "BaisokuRunner "; break;
                case 50: txt = "Undersht "; break;
                case 54: txt = "HumorSense "; break;
                case 57: txt = "EirinCall "; break;
                case 58: txt = "Bullet "; break;
                case 59: txt = "BulletBig "; break;
                case 61: txt = "RShield "; break;
                case 62: txt = "RPanelRepair "; break;
                case 63: txt = "RHoleMake "; break;
                case 64: txt = "LCube "; break;
                case 65: txt = "LMukaikaze "; break;
                case 66: txt = "LOikaze "; break;
                case 67: txt = "LBeastRock "; break;
                case 68: txt = "SlowStart "; break;
                case 69: txt = "NebulaHole "; break;
                case 70: txt = "HPDown100 "; break;
                case 71: txt = "AcidBody "; break;
                case 72: txt = "HideLife "; break;
                case 73: txt = "LostLight "; break;
                case 74: txt = "MassatuSlip "; break;
                case 75: txt = "Meltingth "; break;
                case 76: txt = "CustomPain "; break;
                case 77: txt = "HeavyFoot "; break;
                case 80: txt = "PonkothuBuster "; break;
                case 81: txt = "OwataManBody "; break;
                case 82: txt = "NoGuard "; break;
                case 83: txt = "LostCustom "; break;
                case 84: txt = "BlackMind "; break;
                case 85: txt = "ParizeDamage "; break;
                case 86: txt = "CAuraSword "; break;
                case 87: txt = "CDustBomb "; break;
                case 88: txt = "CVulcan "; break;
                case 89: txt = "CFallKnife "; break;
                case 90: txt = "CBlastCanon "; break;
                case 91: txt = "CLance "; break;
                case 92: txt = "CRepair "; break;
                case 93: txt = "CShotGun "; break;
                case 94: txt = "FudouMyoou "; break;
                case 95: txt = "Scavenger "; break;
                case 96: txt = "Sacrifice "; break;
                case 97: txt = "Mammon "; break;



            }

                return txt;
            
        }


        public static string NumberToAlphabet(int number)
        {
            if (number < 0 || number > 25)
            {
                throw new ArgumentOutOfRangeException("number", "Number must be between 0 and 25.");
            }

            // Convert the number to the corresponding letter (0 -> A, 1 -> B, ..., 25 -> Z)
            return ((char)('A' + number)).ToString();
        }

        public static string RemoveBeforeLastComma(string input)
        {
            // Find the last index of the comma
            int lastCommaIndex = input.LastIndexOf(',');

            // If there's no comma, return the original string
            if (lastCommaIndex == -1)
            {
                return input;
            }

            // Return the substring after the last comma
            return input.Substring(lastCommaIndex + 1);
        }


        public static int FindInSecondColumn(int[,] array, int number)
        {
            // Iterate through each row and check the value in the second column (index 1)
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, 1] == number)
                {
                    return i; // Return the index of the row where the number is found
                }
            }
            return -1; // Return -1 if the number is not found in the second column
        }

        public static int FindInColumn(int[,] array, int number, int column)
        {
            // Iterate through each row and check the value in the second column (index 1)
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, column] == number)
                {
                    return i; // Return the index of the row where the number is found
                }
            }
            return -1; // Return -1 if the number is not found in the second column
        }


        public static (string[], string[]) ShuffleArrays(string[] array1, string[] array2, int seed)
        {
            // Create a list to hold the combined elements of both arrays
            List<string> combinedList = new List<string>();

            // Add elements from both arrays to the list
            combinedList.AddRange(array1);
            combinedList.AddRange(array2);

            // Initialize the Random object with the seed
            Random rng = new Random(seed);

            // Shuffle the combined list using Fisher-Yates algorithm
            int n = combinedList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1); // Get a random index between 0 and n
                string value = combinedList[k];
                combinedList[k] = combinedList[n];
                combinedList[n] = value;
            }

            // Now split the shuffled list back into two arrays with their original sizes
            string[] shuffledArray1 = combinedList.GetRange(0, array1.Length).ToArray();
            string[] shuffledArray2 = combinedList.GetRange(array1.Length, array2.Length).ToArray();

            // Return the shuffled arrays
            return (shuffledArray1, shuffledArray2);
        }


        public static string[] RemoveBlankEntries(string[] array)
        {
            // Use LINQ to filter out null or empty strings from the array
            return array.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        }


        public static int CountCommas(string input)
        {
            int count = 0;

            foreach (char c in input)
            {
                if (c == ',')
                {
                    count++;
                }
            }

            return count;
        }


        static string[] ShuffleArray(string[] array, int seed)
        {
            string[] shuffled = (string[])array.Clone();  // Clone the array to avoid modifying the original one
            Random rng = new Random(seed); // Create a random number generator with a specific seed
            int n = shuffled.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1); // Get a random index between 0 and n
                                         // Swap shuffled[n] with the element at random index k
                string value = shuffled[k];
                shuffled[k] = shuffled[n];
                shuffled[n] = value;
            }
            return shuffled; // Return the shuffled array
        }

        static void Shuffle(string[] array, Random rng)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                // Swap
                string temp = array[k];
                array[k] = array[n];
                array[n] = temp;
            }
        }

        static int FindIndex(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                {
                    return i;  // Return the index where the item was found
                }
            }
            return -1;  // Return -1 if the item is not found
        }

        #endregion  
    }
}
