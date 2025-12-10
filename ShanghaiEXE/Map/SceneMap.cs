using NSAddOn;
using NSBattle.Character;
using NSShanghaiEXE.InputOutput.Audio;
using NSShanghaiEXE.InputOutput.Rendering;
using NSEvent;
using NSGame;
using NSMap.Character;
using Common.Vectors;
using System;
using System.Drawing;
using System.Collections.Generic;
using Data;

namespace NSMap
{
    public class SceneMap : SceneBase
    {
        public int loadflame = 120;
        public Color fadeColor = new Color();
        public int[] stepover = new int[2];
        public byte battlecouunt = 0;
        public float alpha = 0.0f;
        public bool outoCamera = true;
        private byte endAlpha = 0;
        private byte endR = 0;
        private byte endG = 0;
        private byte endB = 0;
        public bool battleflag;
        public SceneMap.STEPS step;
        public SceneMain main;
        public EventManager eventmanager;
        public EventManager eventmanagerParallel;
        private NSMap.Character.Player player;
        private MapField field;
        public HPGauge HP;
        private byte phoneAnime;
        public DebugMode debugmode;
        public bool setCameraOn;
        public Vector3 setCamera;
        public Vector2 cameraPlus;
        public bool hideStatus;
        private bool mailsound;
        private int mailflame;
        private int mailloop;
        private bool mailflag;
        private bool fadeflug;
        private float R;
        private float G;
        private float B;
        private float plusAlpha;
        private float plusR;
        private float plusG;
        private float plusB;
        private int fadeFlame;
        private int fadeTime;
        internal List<IPersistentEvent> persistentEvents;

        public bool NoEvent
        {
            get
            {
                foreach (MapEventBase mapEventBase in this.field.Events)
                {
                    if (mapEventBase.LunPage.startterms == EventPage.STARTTERMS.Auto)
                        return false;
                }
                return true;
            }
        }

        public int MapsizeX
        {
            get
            {
                return this.field.MapsizeX;
            }
        }

        public int MapsizeY
        {
            get
            {
                return this.field.MapsizeY;
            }
        }

        public NSMap.Character.Player Player
        {
            get
            {
                return this.player;
            }
        }

        public MapField Field
        {
            get
            {
                return this.field;
            }
        }

        public byte PhoneAnime
        {
            get
            {
                return this.phoneAnime;
            }
            set
            {
                this.phoneAnime = value;
                if (this.phoneAnime < 4)
                    return;
                this.phoneAnime = 0;
            }
        }

        private bool DebugOn
        {
            get
            {
                return this.debugmode != null && this.debugmode.menuOn;
            }
        }

        public SceneMap(IAudioEngine s, ShanghaiEXE p, SceneMain m, EventManager e, SaveData save)
          : base(s, p, save)
        {
            this.main = m;
            this.eventmanager = new EventManager(this, this.sound);
            this.eventmanagerParallel = new EventManager(this, this.sound);
            this.HP = new HPGauge(this.sound, this.savedata.HPNow, this.savedata.HPMax);
            this.persistentEvents = new List<IPersistentEvent>();
        }

        public void NewGame(int plus) //actual new game, if rando is off
        {



            this.player = new NSMap.Character.Player(this.sound, this, new Point(32, 32), 0, MapCharacterBase.ANGLE.DOWN, this.main, this.savedata, 0.0f);
            this.field = new MapField(this.sound, "aliceroom", this.savedata, this);
            this.savedata.isJackedIn = false;
            if (plus >= 0)
            {
                this.savedata.ValList[28] = plus;
            }
            else
            {
                switch (plus)
                {
                    case -2:
                        this.savedata.GetAddon(new OwataManBody(AddOnBase.ProgramColor.dark));
                        break;
                    case -1:
                        this.savedata.GetAddon(new Haisui(AddOnBase.ProgramColor.dark));
                        this.savedata.GetAddon(new RShield(AddOnBase.ProgramColor.red));
                        this.savedata.GetAddon(new LBeastRock(AddOnBase.ProgramColor.gleen));
                        break;
                }
            }
            bool sfmode = false;
            if (plus != 0)
            {
                sfmode = true;   
            }

            this.player.FieldSet(this.field);
            this.fadeColor = Color.Black;
            this.alpha = byte.MaxValue;
            this.player.NoPrint = true;
            this.savedata.Init_newgame(sfmode);

        }

        public void LoadGame()
        {
            Console.WriteLine("Attempting regular load?");
            this.player = new NSMap.Character.Player(this.sound, this, new Point((int)this.savedata.nowX, (int)this.savedata.nowY), this.savedata.nowFroor, MapCharacterBase.ANGLE.DOWNRIGHT, this.main, this.savedata, this.savedata.nowZ);
            this.step = (SceneMap.STEPS)this.savedata.steptype;
            this.stepover[0] = this.savedata.stepoverX;
            this.stepover[1] = this.savedata.stepoverY;
            this.player.stepCounter = this.savedata.stepCounter;
            this.persistentEvents.Clear();
            this.field = new MapField(this.sound, this.savedata.nowMap, this.savedata, this);
            this.player.FieldSet(this.field);
            this.fadeColor = Color.Black;
            this.alpha = byte.MaxValue;
            this.FadeStart(Color.FromArgb(0, this.fadeColor), 5);



        }

        public void LoadGame2(bool sfmode, bool rndmfoldr) //start randomizer
        {
            Console.WriteLine("Attempting newgame plus load (and setting position to alice's house)");
            this.savedata.isJackedIn = false; //change player to alice, don't run around the overworld
            this.savedata.FlagList[2] = false; //as shanghai (it probobly breaks things?)
            this.player = new NSMap.Character.Player(this.sound, this, new Point((int)30.0, (int)30.0), this.savedata.nowFroor, MapCharacterBase.ANGLE.DOWN, this.main, this.savedata, this.savedata.nowZ);
            this.step = (SceneMap.STEPS)this.savedata.steptype;
            this.stepover[0] = this.savedata.stepoverX;
            this.stepover[1] = this.savedata.stepoverY;
            // this.player.stepCounter = this.savedata.stepCounter;
            this.player.stepCounter = (int)0.0;
            this.persistentEvents.Clear();
            Console.WriteLine(this.savedata.nowMap);
            this.field = new MapField(this.sound, "aliceroom", this.savedata, this);
            this.player.FieldSet(this.field);
            this.fadeColor = Color.Black;
            this.alpha = byte.MaxValue;
            Console.WriteLine("Initing most of ths save file");
            this.savedata.Init(false); //<--- resets flags, put proper flags down bellow

            //this.main.FolderReset();
            this.savedata.chipFolder[0, 0, 0] = 2;

            Console.WriteLine("Setting folder to starting folder(ish)");
            this.main.FolderSave();
            int index1 = 0;
            for (int index2 = 0; index2 < this.savedata.chipFolder.GetLength(1); ++index2)
            {
                for (int index3 = 0; index3 < this.savedata.chipFolder.GetLength(2); ++index3)
                {
                    if (index2 < 3)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 1;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 6)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 43;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 1;
                    }
                    else if (index2 < 8)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 59;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 10)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 62;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 1;
                    }
                    else if (index2 < 12)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 100;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 1;
                    }
                    else if (index2 < 14)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 121;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 2;
                    }
                    else if (index2 < 16)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 100;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 1;
                    }
                    else if (index2 < 17)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 136;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 2;
                    }
                    else if (index2 < 19)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 62;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 22)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 158;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 25)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 174;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 26)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 177;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 27)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 188;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 29)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 188;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                    else if (index2 < 30)
                    {
                        if (index3 == 0)
                            this.savedata.chipFolder[index1, index2, index3] = 190;
                        else
                            this.savedata.chipFolder[index1, index2, index3] = 0;
                    }
                }
            }
            this.savedata.havefolder[1] = true;

            


            this.main.FolderLoad();

            //flag list extracted from a no sidecontent playthrough
            //no idea what most of this shit does, i don't think anybody does!



            #region flag list
            this.savedata.flagList[0] = false;
            this.savedata.flagList[1] = false;
            this.savedata.flagList[2] = false;
            this.savedata.flagList[3] = false;
            this.savedata.flagList[4] = false;
            this.savedata.flagList[5] = true;
            this.savedata.flagList[6] = true;
            this.savedata.flagList[7] = false;
            this.savedata.flagList[8] = false;
            this.savedata.flagList[9] = false;
            this.savedata.flagList[10] = true;
            this.savedata.flagList[11] = true;
            this.savedata.flagList[12] = true;
            this.savedata.flagList[13] = false;
            this.savedata.flagList[14] = true;
            this.savedata.flagList[15] = true;
            this.savedata.flagList[16] = false;
            this.savedata.flagList[17] = true;
            this.savedata.flagList[18] = false;
            this.savedata.flagList[19] = true;
            this.savedata.flagList[20] = true;
            this.savedata.flagList[21] = true;
            this.savedata.flagList[22] = true;
            this.savedata.flagList[23] = true;
            this.savedata.flagList[24] = true;
            this.savedata.flagList[25] = false;
            this.savedata.flagList[26] = true;
            this.savedata.flagList[27] = true;
            this.savedata.flagList[28] = false;
            this.savedata.flagList[29] = true;
            this.savedata.flagList[30] = true;
            this.savedata.flagList[31] = true;
            this.savedata.flagList[32] = true;
            this.savedata.flagList[33] = true;
            this.savedata.flagList[34] = true;
            this.savedata.flagList[35] = false;
            this.savedata.flagList[36] = true;
            this.savedata.flagList[37] = true;
            this.savedata.flagList[38] = true;
            this.savedata.flagList[39] = true;
            this.savedata.flagList[40] = false;
            this.savedata.flagList[41] = false;
            this.savedata.flagList[42] = false;
            this.savedata.flagList[43] = false;
            this.savedata.flagList[44] = false;
            this.savedata.flagList[45] = false;
            this.savedata.flagList[46] = false;
            this.savedata.flagList[47] = false;
            this.savedata.flagList[48] = false;
            this.savedata.flagList[49] = false;
            this.savedata.flagList[50] = false;
            this.savedata.flagList[51] = false;
            this.savedata.flagList[52] = false;
            this.savedata.flagList[53] = false;
            this.savedata.flagList[54] = false;
            this.savedata.flagList[55] = false;
            this.savedata.flagList[56] = false;
            this.savedata.flagList[57] = false;
            this.savedata.flagList[58] = false;
            this.savedata.flagList[59] = false;
            this.savedata.flagList[60] = false;
            this.savedata.flagList[61] = false;
            this.savedata.flagList[62] = false;
            this.savedata.flagList[63] = false;
            this.savedata.flagList[64] = false;
            this.savedata.flagList[65] = false;
            this.savedata.flagList[66] = false;
            this.savedata.flagList[67] = false;
            this.savedata.flagList[68] = false;
            this.savedata.flagList[69] = false;
            this.savedata.flagList[70] = false;
            this.savedata.flagList[71] = true;
            this.savedata.flagList[72] = true;
            this.savedata.flagList[73] = true;
            this.savedata.flagList[74] = true;
            this.savedata.flagList[75] = false;
            this.savedata.flagList[76] = true;
            this.savedata.flagList[77] = true;
            this.savedata.flagList[78] = true;
            this.savedata.flagList[79] = true;
            this.savedata.flagList[80] = false;
            this.savedata.flagList[81] = false;
            this.savedata.flagList[82] = false;
            this.savedata.flagList[83] = false;
            this.savedata.flagList[84] = false;
            this.savedata.flagList[85] = false;
            this.savedata.flagList[86] = false;
            this.savedata.flagList[87] = false;
            this.savedata.flagList[88] = false;
            this.savedata.flagList[89] = false;
            this.savedata.flagList[90] = false;
            this.savedata.flagList[91] = false;
            this.savedata.flagList[92] = false;
            this.savedata.flagList[93] = false;
            this.savedata.flagList[94] = false;
            this.savedata.flagList[95] = false;
            this.savedata.flagList[96] = false;
            this.savedata.flagList[97] = false;
            this.savedata.flagList[98] = false;
            this.savedata.flagList[99] = false;
            this.savedata.flagList[100] = false;
            this.savedata.flagList[101] = false;
            this.savedata.flagList[102] = false;
            this.savedata.flagList[103] = false;
            this.savedata.flagList[104] = false;
            this.savedata.flagList[105] = false;
            this.savedata.flagList[106] = false;
            this.savedata.flagList[107] = false;
            this.savedata.flagList[108] = false;
            this.savedata.flagList[109] = false;
            this.savedata.flagList[110] = false;
            this.savedata.flagList[111] = false;
            this.savedata.flagList[112] = false;
            this.savedata.flagList[113] = false;
            this.savedata.flagList[114] = false;
            this.savedata.flagList[115] = false;
            this.savedata.flagList[116] = false;
            this.savedata.flagList[117] = false;
            this.savedata.flagList[118] = false;
            this.savedata.flagList[119] = false;
            this.savedata.flagList[120] = true;
            this.savedata.flagList[121] = true;
            this.savedata.flagList[122] = true;
            this.savedata.flagList[123] = true;
            this.savedata.flagList[124] = true;
            this.savedata.flagList[125] = true;
            this.savedata.flagList[126] = true;
            this.savedata.flagList[127] = true;
            this.savedata.flagList[128] = true;
            this.savedata.flagList[129] = true;
            this.savedata.flagList[130] = true;
            this.savedata.flagList[131] = true;
            this.savedata.flagList[132] = true;
            this.savedata.flagList[133] = true;
            this.savedata.flagList[134] = true;
            this.savedata.flagList[135] = true;
            this.savedata.flagList[136] = true;
            this.savedata.flagList[137] = true;
            this.savedata.flagList[138] = true;
            this.savedata.flagList[139] = true;
            this.savedata.flagList[140] = true;
            this.savedata.flagList[141] = true;
            this.savedata.flagList[142] = false;
            this.savedata.flagList[143] = true;
            this.savedata.flagList[144] = true;
            this.savedata.flagList[145] = true;
            this.savedata.flagList[146] = true;
            this.savedata.flagList[147] = true;
            this.savedata.flagList[148] = false;
            this.savedata.flagList[149] = false;
            this.savedata.flagList[150] = false;
            this.savedata.flagList[151] = false;
            this.savedata.flagList[152] = false;
            this.savedata.flagList[153] = false;
            this.savedata.flagList[154] = false;
            this.savedata.flagList[155] = false;
            this.savedata.flagList[156] = true;
            this.savedata.flagList[157] = true;
            this.savedata.flagList[158] = true;
            this.savedata.flagList[159] = true;
            this.savedata.flagList[160] = false;
            this.savedata.flagList[161] = false;
            this.savedata.flagList[162] = false;
            this.savedata.flagList[163] = false;
            this.savedata.flagList[164] = false;
            this.savedata.flagList[165] = false;
            this.savedata.flagList[166] = false;
            this.savedata.flagList[167] = false;
            this.savedata.flagList[168] = false;
            this.savedata.flagList[169] = false;
            this.savedata.flagList[170] = false;
            this.savedata.flagList[171] = false;
            this.savedata.flagList[172] = false;
            this.savedata.flagList[173] = false;
            this.savedata.flagList[174] = false;
            this.savedata.flagList[175] = false;
            this.savedata.flagList[176] = false;
            this.savedata.flagList[177] = false;
            this.savedata.flagList[178] = false;
            this.savedata.flagList[179] = false;
            this.savedata.flagList[180] = true;
            this.savedata.flagList[181] = true;
            this.savedata.flagList[182] = true;
            this.savedata.flagList[183] = true;
            this.savedata.flagList[184] = true;
            this.savedata.flagList[185] = false;
            this.savedata.flagList[186] = false;
            this.savedata.flagList[187] = false;
            this.savedata.flagList[188] = false;
            this.savedata.flagList[189] = false;
            this.savedata.flagList[190] = true;
            this.savedata.flagList[191] = true;
            this.savedata.flagList[192] = true;
            this.savedata.flagList[193] = true;
            this.savedata.flagList[194] = true;
            this.savedata.flagList[195] = true;
            this.savedata.flagList[196] = false;
            this.savedata.flagList[197] = true;
            this.savedata.flagList[198] = true;
            this.savedata.flagList[199] = true;
            this.savedata.flagList[200] = true;
            this.savedata.flagList[201] = true;
            this.savedata.flagList[202] = true;
            this.savedata.flagList[203] = true;
            this.savedata.flagList[204] = true;
            this.savedata.flagList[205] = true;
            this.savedata.flagList[206] = true;
            this.savedata.flagList[207] = true;
            this.savedata.flagList[208] = true;
            this.savedata.flagList[209] = true;
            this.savedata.flagList[210] = true;
            this.savedata.flagList[211] = false;
            this.savedata.flagList[212] = true;
            this.savedata.flagList[213] = true;
            this.savedata.flagList[214] = true;
            this.savedata.flagList[215] = true;
            this.savedata.flagList[216] = true;
            this.savedata.flagList[217] = true;
            this.savedata.flagList[218] = true;
            this.savedata.flagList[219] = true;
            this.savedata.flagList[220] = true;
            this.savedata.flagList[221] = true;
            this.savedata.flagList[222] = true;
            this.savedata.flagList[223] = true;
            this.savedata.flagList[224] = false;
            this.savedata.flagList[225] = true;
            this.savedata.flagList[226] = true;
            this.savedata.flagList[227] = true;
            this.savedata.flagList[228] = true;
            this.savedata.flagList[229] = true;
            this.savedata.flagList[230] = true;
            this.savedata.flagList[231] = true;
            this.savedata.flagList[232] = true;
            this.savedata.flagList[233] = true;
            this.savedata.flagList[234] = true;
            this.savedata.flagList[235] = true;
            this.savedata.flagList[236] = true;
            this.savedata.flagList[237] = true;
            this.savedata.flagList[238] = true;
            this.savedata.flagList[239] = true;
            this.savedata.flagList[240] = false;
            this.savedata.flagList[241] = true;
            this.savedata.flagList[242] = true;
            this.savedata.flagList[243] = true;
            this.savedata.flagList[244] = true;
            this.savedata.flagList[245] = true;
            this.savedata.flagList[246] = false;
            this.savedata.flagList[247] = true;
            this.savedata.flagList[248] = true;
            this.savedata.flagList[249] = true;
            this.savedata.flagList[250] = true;
            this.savedata.flagList[251] = true;
            this.savedata.flagList[252] = true;
            this.savedata.flagList[253] = false;
            this.savedata.flagList[254] = true;
            this.savedata.flagList[255] = true;
            this.savedata.flagList[256] = true;
            this.savedata.flagList[257] = true;
            this.savedata.flagList[258] = true;
            this.savedata.flagList[259] = true;
            this.savedata.flagList[260] = true;
            this.savedata.flagList[261] = true;
            this.savedata.flagList[262] = true;
            this.savedata.flagList[263] = false;
            this.savedata.flagList[264] = true;
            this.savedata.flagList[265] = true;
            this.savedata.flagList[266] = true;
            this.savedata.flagList[267] = true;
            this.savedata.flagList[268] = false;
            this.savedata.flagList[269] = false;
            this.savedata.flagList[270] = false;
            this.savedata.flagList[271] = false;
            this.savedata.flagList[272] = false;
            this.savedata.flagList[273] = false;
            this.savedata.flagList[274] = false;
            this.savedata.flagList[275] = false;
            this.savedata.flagList[276] = true;
            this.savedata.flagList[277] = true;
            this.savedata.flagList[278] = true;
            this.savedata.flagList[279] = true;
            this.savedata.flagList[280] = false;
            this.savedata.flagList[281] = true;
            this.savedata.flagList[282] = true;
            this.savedata.flagList[283] = true;
            this.savedata.flagList[284] = false;
            this.savedata.flagList[285] = false;
            this.savedata.flagList[286] = false;
            this.savedata.flagList[287] = false;
            this.savedata.flagList[288] = false;
            this.savedata.flagList[289] = false;
            this.savedata.flagList[290] = false;
            this.savedata.flagList[291] = true;
            this.savedata.flagList[292] = true;
            this.savedata.flagList[293] = true;
            this.savedata.flagList[294] = true;
            this.savedata.flagList[295] = true;
            this.savedata.flagList[296] = true;
            this.savedata.flagList[297] = true;
            this.savedata.flagList[298] = true;
            this.savedata.flagList[299] = true;
            this.savedata.flagList[300] = false;
            this.savedata.flagList[301] = false;
            this.savedata.flagList[302] = false;
            this.savedata.flagList[303] = false;
            this.savedata.flagList[304] = false;
            this.savedata.flagList[305] = false;
            this.savedata.flagList[306] = false;
            this.savedata.flagList[307] = false;
            this.savedata.flagList[308] = false;
            this.savedata.flagList[309] = false;
            this.savedata.flagList[310] = false;
            this.savedata.flagList[311] = false;
            this.savedata.flagList[312] = false;
            this.savedata.flagList[313] = false;
            this.savedata.flagList[314] = false;
            this.savedata.flagList[315] = false;
            this.savedata.flagList[316] = false;
            this.savedata.flagList[317] = false;
            this.savedata.flagList[318] = false;
            this.savedata.flagList[319] = false;
            this.savedata.flagList[320] = false;
            this.savedata.flagList[321] = false;
            this.savedata.flagList[322] = false;
            this.savedata.flagList[323] = false;
            this.savedata.flagList[324] = false;
            this.savedata.flagList[325] = false;
            this.savedata.flagList[326] = false;
            this.savedata.flagList[327] = false;
            this.savedata.flagList[328] = false;
            this.savedata.flagList[329] = false;
            this.savedata.flagList[330] = false;
            this.savedata.flagList[331] = false;
            this.savedata.flagList[332] = false;
            this.savedata.flagList[333] = false;
            this.savedata.flagList[334] = false;
            this.savedata.flagList[335] = false;
            this.savedata.flagList[336] = false;
            this.savedata.flagList[337] = false;
            this.savedata.flagList[338] = false;
            this.savedata.flagList[339] = false;
            this.savedata.flagList[340] = false;
            this.savedata.flagList[341] = false;
            this.savedata.flagList[342] = false;
            this.savedata.flagList[343] = false;
            this.savedata.flagList[344] = false;
            this.savedata.flagList[345] = false;
            this.savedata.flagList[346] = false;
            this.savedata.flagList[347] = false;
            this.savedata.flagList[348] = false;
            this.savedata.flagList[349] = false;
            this.savedata.flagList[350] = false;
            this.savedata.flagList[351] = false;
            this.savedata.flagList[352] = false;
            this.savedata.flagList[353] = false;
            this.savedata.flagList[354] = false;
            this.savedata.flagList[355] = false;
            this.savedata.flagList[356] = false;
            this.savedata.flagList[357] = false;
            this.savedata.flagList[358] = false;
            this.savedata.flagList[359] = false;
            this.savedata.flagList[360] = false;
            this.savedata.flagList[361] = false;
            this.savedata.flagList[362] = false;
            this.savedata.flagList[363] = false;
            this.savedata.flagList[364] = false;
            this.savedata.flagList[365] = false;
            this.savedata.flagList[366] = false;
            this.savedata.flagList[367] = false;
            this.savedata.flagList[368] = false;
            this.savedata.flagList[369] = false;
            this.savedata.flagList[370] = false;
            this.savedata.flagList[371] = false;
            this.savedata.flagList[372] = false;
            this.savedata.flagList[373] = false;
            this.savedata.flagList[374] = false;
            this.savedata.flagList[375] = false;
            this.savedata.flagList[376] = false;
            this.savedata.flagList[377] = false;
            this.savedata.flagList[378] = false;
            this.savedata.flagList[379] = false;
            this.savedata.flagList[380] = false;
            this.savedata.flagList[381] = false;
            this.savedata.flagList[382] = false;
            this.savedata.flagList[383] = false;
            this.savedata.flagList[384] = false;
            this.savedata.flagList[385] = false;
            this.savedata.flagList[386] = false;
            this.savedata.flagList[387] = false;
            this.savedata.flagList[388] = false;
            this.savedata.flagList[389] = false;
            this.savedata.flagList[390] = false;
            this.savedata.flagList[391] = false;
            this.savedata.flagList[392] = false;
            this.savedata.flagList[393] = false;
            this.savedata.flagList[394] = false;
            this.savedata.flagList[395] = false;
            this.savedata.flagList[396] = false;
            this.savedata.flagList[397] = false;
            this.savedata.flagList[398] = false;
            this.savedata.flagList[399] = false;
            this.savedata.flagList[400] = false;
            this.savedata.flagList[401] = false;
            this.savedata.flagList[402] = false;
            this.savedata.flagList[403] = false;
            this.savedata.flagList[404] = false;
            this.savedata.flagList[405] = false;
            this.savedata.flagList[406] = false;
            this.savedata.flagList[407] = false;
            this.savedata.flagList[408] = false;
            this.savedata.flagList[409] = false;
            this.savedata.flagList[410] = false;
            this.savedata.flagList[411] = false;
            this.savedata.flagList[412] = false;
            this.savedata.flagList[413] = false;
            this.savedata.flagList[414] = false;
            this.savedata.flagList[415] = false;
            this.savedata.flagList[416] = false;
            this.savedata.flagList[417] = false;
            this.savedata.flagList[418] = false;
            this.savedata.flagList[419] = false;
            this.savedata.flagList[420] = true;
            this.savedata.flagList[421] = true;
            this.savedata.flagList[422] = true;
            this.savedata.flagList[423] = false;
            this.savedata.flagList[424] = false;
            this.savedata.flagList[425] = false;
            this.savedata.flagList[426] = false;
            this.savedata.flagList[427] = false;
            this.savedata.flagList[428] = true;
            this.savedata.flagList[429] = true;
            this.savedata.flagList[430] = true;
            this.savedata.flagList[431] = true;
            this.savedata.flagList[432] = true;
            this.savedata.flagList[433] = true;
            this.savedata.flagList[434] = true;
            this.savedata.flagList[435] = true;
            this.savedata.flagList[436] = true;
            this.savedata.flagList[437] = true;
            this.savedata.flagList[438] = true;
            this.savedata.flagList[439] = false;
            this.savedata.flagList[440] = true;
            this.savedata.flagList[441] = true;
            this.savedata.flagList[442] = true;
            this.savedata.flagList[443] = true;
            this.savedata.flagList[444] = false;
            this.savedata.flagList[445] = true;
            this.savedata.flagList[446] = true;
            this.savedata.flagList[447] = true;
            this.savedata.flagList[448] = true;
            this.savedata.flagList[449] = true;
            this.savedata.flagList[450] = true;
            this.savedata.flagList[451] = true;
            this.savedata.flagList[452] = true;
            this.savedata.flagList[453] = true;
            this.savedata.flagList[454] = true;
            this.savedata.flagList[455] = true;
            this.savedata.flagList[456] = true;
            this.savedata.flagList[457] = true;
            this.savedata.flagList[458] = true;
            this.savedata.flagList[459] = true;
            this.savedata.flagList[460] = false;
            this.savedata.flagList[461] = false;
            this.savedata.flagList[462] = false;
            this.savedata.flagList[463] = false;
            this.savedata.flagList[464] = false;
            this.savedata.flagList[465] = false;
            this.savedata.flagList[466] = false;
            this.savedata.flagList[467] = false;
            this.savedata.flagList[468] = false;
            this.savedata.flagList[469] = false;
            this.savedata.flagList[470] = false;
            this.savedata.flagList[471] = false;
            this.savedata.flagList[472] = false;
            this.savedata.flagList[473] = false;
            this.savedata.flagList[474] = false;
            this.savedata.flagList[475] = false;
            this.savedata.flagList[476] = false;
            this.savedata.flagList[477] = false;
            this.savedata.flagList[478] = false;
            this.savedata.flagList[479] = false;
            this.savedata.flagList[480] = false;
            this.savedata.flagList[481] = false;
            this.savedata.flagList[482] = false;
            this.savedata.flagList[483] = false;
            this.savedata.flagList[484] = false;
            this.savedata.flagList[485] = false;
            this.savedata.flagList[486] = false;
            this.savedata.flagList[487] = false;
            this.savedata.flagList[488] = false;
            this.savedata.flagList[489] = false;
            this.savedata.flagList[490] = false;
            this.savedata.flagList[491] = false;
            this.savedata.flagList[492] = false;
            this.savedata.flagList[493] = false;
            this.savedata.flagList[494] = false;
            this.savedata.flagList[495] = false;
            this.savedata.flagList[496] = false;
            this.savedata.flagList[497] = false;
            this.savedata.flagList[498] = false;
            this.savedata.flagList[499] = false;
            this.savedata.flagList[500] = true;
            this.savedata.flagList[501] = true;
            this.savedata.flagList[502] = true;
            this.savedata.flagList[503] = true;
            this.savedata.flagList[504] = true;
            this.savedata.flagList[505] = true;
            this.savedata.flagList[506] = true;
            this.savedata.flagList[507] = true;
            this.savedata.flagList[508] = true;
            this.savedata.flagList[509] = true;
            this.savedata.flagList[510] = true;
            this.savedata.flagList[511] = true;
            this.savedata.flagList[512] = true;
            this.savedata.flagList[513] = true;
            this.savedata.flagList[514] = true;
            this.savedata.flagList[515] = true;
            this.savedata.flagList[516] = true;
            this.savedata.flagList[517] = true;
            this.savedata.flagList[518] = true;
            this.savedata.flagList[519] = false;
            this.savedata.flagList[520] = true;
            this.savedata.flagList[521] = true;
            this.savedata.flagList[522] = true;
            this.savedata.flagList[523] = true;
            this.savedata.flagList[524] = true;
            this.savedata.flagList[525] = true;
            this.savedata.flagList[526] = true;
            this.savedata.flagList[527] = true;
            this.savedata.flagList[528] = true;
            this.savedata.flagList[529] = true;
            this.savedata.flagList[530] = true;
            this.savedata.flagList[531] = true;
            this.savedata.flagList[532] = true;
            this.savedata.flagList[533] = true;
            this.savedata.flagList[534] = true;
            this.savedata.flagList[535] = true;
            this.savedata.flagList[536] = true;
            this.savedata.flagList[537] = true;
            this.savedata.flagList[538] = true;
            this.savedata.flagList[539] = true;
            this.savedata.flagList[540] = true;
            this.savedata.flagList[541] = true;
            this.savedata.flagList[542] = false;
            this.savedata.flagList[543] = true;
            this.savedata.flagList[544] = true;
            this.savedata.flagList[545] = false;
            this.savedata.flagList[546] = true;
            this.savedata.flagList[547] = true;
            this.savedata.flagList[548] = true;
            this.savedata.flagList[549] = true;
            this.savedata.flagList[550] = true;
            this.savedata.flagList[551] = true;
            this.savedata.flagList[552] = true;
            this.savedata.flagList[553] = true;
            this.savedata.flagList[554] = false;
            this.savedata.flagList[555] = true;
            this.savedata.flagList[556] = true;
            this.savedata.flagList[557] = true;
            this.savedata.flagList[558] = true;
            this.savedata.flagList[559] = true;
            this.savedata.flagList[560] = false;
            this.savedata.flagList[561] = false;
            this.savedata.flagList[562] = false;
            this.savedata.flagList[563] = false;
            this.savedata.flagList[564] = false;
            this.savedata.flagList[565] = false;
            this.savedata.flagList[566] = false;
            this.savedata.flagList[567] = false;
            this.savedata.flagList[568] = false;
            this.savedata.flagList[569] = false;
            this.savedata.flagList[570] = false;
            this.savedata.flagList[571] = false;
            this.savedata.flagList[572] = false;
            this.savedata.flagList[573] = false;
            this.savedata.flagList[574] = false;
            this.savedata.flagList[575] = false;
            this.savedata.flagList[576] = false;
            this.savedata.flagList[577] = false;
            this.savedata.flagList[578] = false;
            this.savedata.flagList[579] = false;
            this.savedata.flagList[580] = true;
            this.savedata.flagList[581] = false;
            this.savedata.flagList[582] = true;
            this.savedata.flagList[583] = false;
            this.savedata.flagList[584] = true;
            this.savedata.flagList[585] = false;
            this.savedata.flagList[586] = true;
            this.savedata.flagList[587] = false;
            this.savedata.flagList[588] = true;
            this.savedata.flagList[589] = false;
            this.savedata.flagList[590] = true;
            this.savedata.flagList[591] = true;
            this.savedata.flagList[592] = true;
            this.savedata.flagList[593] = false;
            this.savedata.flagList[594] = true;
            this.savedata.flagList[595] = false;
            this.savedata.flagList[596] = true;
            this.savedata.flagList[597] = true;
            this.savedata.flagList[598] = true;
            this.savedata.flagList[599] = true;
            this.savedata.flagList[600] = true;
            this.savedata.flagList[601] = true;
            this.savedata.flagList[602] = true;
            this.savedata.flagList[603] = true;
            this.savedata.flagList[604] = true;
            this.savedata.flagList[605] = true;
            this.savedata.flagList[606] = true;
            this.savedata.flagList[607] = true;
            this.savedata.flagList[608] = false;
            this.savedata.flagList[609] = true;
            this.savedata.flagList[610] = true;
            this.savedata.flagList[611] = true;
            this.savedata.flagList[612] = true;
            this.savedata.flagList[613] = true;
            this.savedata.flagList[614] = true;
            this.savedata.flagList[615] = true;
            this.savedata.flagList[616] = true;
            this.savedata.flagList[617] = true;
            this.savedata.flagList[618] = false;
            this.savedata.flagList[619] = false;
            this.savedata.flagList[620] = false;
            this.savedata.flagList[621] = false;
            this.savedata.flagList[622] = false;
            this.savedata.flagList[623] = false;
            this.savedata.flagList[624] = false;
            this.savedata.flagList[625] = false;
            this.savedata.flagList[626] = false;
            this.savedata.flagList[627] = false;
            this.savedata.flagList[628] = false;
            this.savedata.flagList[629] = false;
            this.savedata.flagList[630] = false;
            this.savedata.flagList[631] = false;
            this.savedata.flagList[632] = false;
            this.savedata.flagList[633] = false;
            this.savedata.flagList[634] = false;
            this.savedata.flagList[635] = false;
            this.savedata.flagList[636] = false;
            this.savedata.flagList[637] = false;
            this.savedata.flagList[638] = false;
            this.savedata.flagList[639] = false;
            this.savedata.flagList[640] = false;
            this.savedata.flagList[641] = false;
            this.savedata.flagList[642] = false;
            this.savedata.flagList[643] = false;
            this.savedata.flagList[644] = false;
            this.savedata.flagList[645] = false;
            this.savedata.flagList[646] = false;
            this.savedata.flagList[647] = false;
            this.savedata.flagList[648] = false;
            this.savedata.flagList[649] = false;
            this.savedata.flagList[650] = false;
            this.savedata.flagList[651] = false;
            this.savedata.flagList[652] = false;
            this.savedata.flagList[653] = false;
            this.savedata.flagList[654] = false;
            this.savedata.flagList[655] = false;
            this.savedata.flagList[656] = false;
            this.savedata.flagList[657] = false;
            this.savedata.flagList[658] = false;
            this.savedata.flagList[659] = false;
            this.savedata.flagList[660] = false;
            this.savedata.flagList[661] = false;
            this.savedata.flagList[662] = false;
            this.savedata.flagList[663] = false;
            this.savedata.flagList[664] = false;
            this.savedata.flagList[665] = false;
            this.savedata.flagList[666] = false;
            this.savedata.flagList[667] = false;
            this.savedata.flagList[668] = false;
            this.savedata.flagList[669] = false;
            this.savedata.flagList[670] = false;
            this.savedata.flagList[671] = false;
            this.savedata.flagList[672] = false;
            this.savedata.flagList[673] = false;
            this.savedata.flagList[674] = false;
            this.savedata.flagList[675] = false;
            this.savedata.flagList[676] = false;
            this.savedata.flagList[677] = false;
            this.savedata.flagList[678] = false;
            this.savedata.flagList[679] = false;
            this.savedata.flagList[680] = false;
            this.savedata.flagList[681] = false;
            this.savedata.flagList[682] = false;
            this.savedata.flagList[683] = false;
            this.savedata.flagList[684] = false;
            this.savedata.flagList[685] = false;
            this.savedata.flagList[686] = false;
            this.savedata.flagList[687] = false;
            this.savedata.flagList[688] = false;
            this.savedata.flagList[689] = false;
            this.savedata.flagList[690] = false;
            this.savedata.flagList[691] = false;
            this.savedata.flagList[692] = false;
            this.savedata.flagList[693] = false;
            this.savedata.flagList[694] = false;
            this.savedata.flagList[695] = false;
            this.savedata.flagList[696] = false;
            this.savedata.flagList[697] = false;
            this.savedata.flagList[698] = false;
            this.savedata.flagList[699] = false;
            this.savedata.flagList[700] = false;
            this.savedata.flagList[701] = false;
            this.savedata.flagList[702] = false;
            this.savedata.flagList[703] = false;
            this.savedata.flagList[704] = false;
            this.savedata.flagList[705] = false;
            this.savedata.flagList[706] = false;
            this.savedata.flagList[707] = false;
            this.savedata.flagList[708] = false;
            this.savedata.flagList[709] = false;
            this.savedata.flagList[710] = false;
            this.savedata.flagList[711] = false;
            this.savedata.flagList[712] = false;
            this.savedata.flagList[713] = false;
            this.savedata.flagList[714] = false;
            this.savedata.flagList[715] = false;
            this.savedata.flagList[716] = false;
            this.savedata.flagList[717] = false;
            this.savedata.flagList[718] = false;
            this.savedata.flagList[719] = false;
            this.savedata.flagList[720] = false;
            this.savedata.flagList[721] = false;
            this.savedata.flagList[722] = false;
            this.savedata.flagList[723] = false;
            this.savedata.flagList[724] = false;
            this.savedata.flagList[725] = false;
            this.savedata.flagList[726] = false;
            this.savedata.flagList[727] = false;
            this.savedata.flagList[728] = false;
            this.savedata.flagList[729] = false;
            this.savedata.flagList[730] = false;
            this.savedata.flagList[731] = false;
            this.savedata.flagList[732] = false;
            this.savedata.flagList[733] = false;
            this.savedata.flagList[734] = false;
            this.savedata.flagList[735] = false;
            this.savedata.flagList[736] = false;
            this.savedata.flagList[737] = false;
            this.savedata.flagList[738] = false;
            this.savedata.flagList[739] = false;
            this.savedata.flagList[740] = false;
            this.savedata.flagList[741] = false;
            this.savedata.flagList[742] = false;
            this.savedata.flagList[743] = false;
            this.savedata.flagList[744] = false;
            this.savedata.flagList[745] = false;
            this.savedata.flagList[746] = false;
            this.savedata.flagList[747] = false;
            this.savedata.flagList[748] = false;
            this.savedata.flagList[749] = false;
            this.savedata.flagList[750] = false;
            this.savedata.flagList[751] = false;
            this.savedata.flagList[752] = false;
            this.savedata.flagList[753] = false;
            this.savedata.flagList[754] = false;
            this.savedata.flagList[755] = false;
            this.savedata.flagList[756] = false;
            this.savedata.flagList[757] = false;
            this.savedata.flagList[758] = false;
            this.savedata.flagList[759] = false;
            this.savedata.flagList[760] = true;
            this.savedata.flagList[761] = true;
            this.savedata.flagList[762] = true;
            this.savedata.flagList[763] = true;
            this.savedata.flagList[764] = true;
            this.savedata.flagList[765] = true;
            this.savedata.flagList[766] = true;
            this.savedata.flagList[767] = true;
            this.savedata.flagList[768] = true;
            this.savedata.flagList[769] = true;
            this.savedata.flagList[770] = true;
            this.savedata.flagList[771] = true;
            this.savedata.flagList[772] = true;
            this.savedata.flagList[773] = true;
            this.savedata.flagList[774] = true;
            this.savedata.flagList[775] = true;
            this.savedata.flagList[776] = true;
            this.savedata.flagList[777] = true;
            this.savedata.flagList[778] = true;
            this.savedata.flagList[779] = false;
            this.savedata.flagList[780] = true;
            this.savedata.flagList[781] = true;
            this.savedata.flagList[782] = true;
            this.savedata.flagList[783] = true;
            this.savedata.flagList[784] = true;
            this.savedata.flagList[785] = false;
            this.savedata.flagList[786] = true;
            this.savedata.flagList[787] = true;
            this.savedata.flagList[788] = true;
            this.savedata.flagList[789] = true;
            this.savedata.flagList[790] = false;
            this.savedata.flagList[791] = false;
            this.savedata.flagList[792] = false;
            this.savedata.flagList[793] = false;
            this.savedata.flagList[794] = false;
            this.savedata.flagList[795] = false;
            this.savedata.flagList[796] = false;
            this.savedata.flagList[797] = false;
            this.savedata.flagList[798] = false;
            this.savedata.flagList[799] = false;
            this.savedata.flagList[800] = false;
            this.savedata.flagList[801] = false;
            this.savedata.flagList[802] = false;
            this.savedata.flagList[803] = false;
            this.savedata.flagList[804] = false;
            this.savedata.flagList[805] = false;
            this.savedata.flagList[806] = false;
            this.savedata.flagList[807] = false;
            this.savedata.flagList[808] = false;
            this.savedata.flagList[809] = false;
            this.savedata.flagList[810] = false;
            this.savedata.flagList[811] = false;
            this.savedata.flagList[812] = false;
            this.savedata.flagList[813] = false;
            this.savedata.flagList[814] = false;
            this.savedata.flagList[815] = false;
            this.savedata.flagList[816] = false;
            this.savedata.flagList[817] = false;
            this.savedata.flagList[818] = false;
            this.savedata.flagList[819] = false;
            this.savedata.flagList[820] = false;
            this.savedata.flagList[821] = false;
            this.savedata.flagList[822] = false;
            this.savedata.flagList[823] = false;
            this.savedata.flagList[824] = false;
            this.savedata.flagList[825] = false;
            this.savedata.flagList[826] = false;
            this.savedata.flagList[827] = false;
            this.savedata.flagList[828] = false;
            this.savedata.flagList[829] = false;
            this.savedata.flagList[830] = false;
            this.savedata.flagList[831] = false;
            this.savedata.flagList[832] = false;
            this.savedata.flagList[833] = false;
            this.savedata.flagList[834] = false;
            this.savedata.flagList[835] = false;
            this.savedata.flagList[836] = false;
            this.savedata.flagList[837] = false;
            this.savedata.flagList[838] = true;
            this.savedata.flagList[839] = true;
            this.savedata.flagList[840] = false;
            this.savedata.flagList[841] = false;
            this.savedata.flagList[842] = true;

            this.savedata.flagList[884] = false;
            #endregion



            this.savedata.flagList[788] = false; //stage intro cutscene

            //this.savedata.flagList[722] = true; //start hospital
            //this.savedata.flagList[721] = true;
            //this.savedata.flagList[720] = true;

            this.savedata.ValList[10] = 8; //set worldstate to endgame
            this.savedata.ValList[11] = -1; //no active quests
            this.savedata.ValList[12] = -1; //no active SP target
            this.savedata.ValList[35] = 1; //set bosses to appear as they do in a normal game (why is this even a var?)

            this.savedata.ValList[3] = 102; //set hint to heaven hint
            this.savedata.ValList[199] = 8; //set savedata to version from final anon release, for compatability
            
            Console.WriteLine("Zeroing out BMD data");
            for (int index45 = 0; index45 < 600; ++index45) //zero out BMD
            {
                this.savedata.GetMystery[index45] = false;
                this.savedata.GetRandomMystery[index45] = false;
            }

            Console.WriteLine("Set flags here, when we figgure them out");

            this.FadeStart(Color.FromArgb(0, this.fadeColor), 5);

            if (sfmode == true)
            {
                this.savedata.GetAddon(new Haisui(AddOnBase.ProgramColor.dark));
                this.savedata.GetAddon(new RShield(AddOnBase.ProgramColor.red));
                this.savedata.GetAddon(new LBeastRock(AddOnBase.ProgramColor.gleen));
            }

            bool random2ndfolder = false;

            if (rndmfoldr == true)
            {
                Console.WriteLine("Shuffling random folder");
                index1 = 0;
                //var index3 = 0;
                var seed = ShanghaiEXE.Config.Seed;
                Random rng = new Random(seed);
                
                //TODO: library this so it dosn't generate unsueable folders or otherwise unobtainable chips

                for (int index2 = 0; index2 < this.savedata.chipFolder.GetLength(1); ++index2)
                {
                    for (int index3 = 0; index3 < this.savedata.chipFolder.GetLength(2); ++index3)
                    {
                        int chipno = Random.Next(1, 50);
                        int chipcode = Random.Next(0, 3);
                        if (index3 == 0)
                            this.savedata.chipFolder[0, index2, index3] = chipno;
                        else if (index3 == 1)
                            this.savedata.chipFolder[0, index2, index3] = chipcode;

                        if (random2ndfolder == true)
                        {
                            chipno = Random.Next(1, 50);
                            chipcode = Random.Next(0, 3);
                            if (index3 == 0)
                                this.savedata.chipFolder[1, index2, index3] = chipno;
                            else if (index3 == 1)
                                this.savedata.chipFolder[1, index2, index3] = chipcode;
                        }
                        //this.savedata.chipFolder[index1, index2, index3] = chipno;
                        //Console.WriteLine(chipno);
                        //var code = Random.Next(0, 3);
                        //this.savedata.chipFolder[index1, index2, 1] = code;

                    }
                }
                
                
                
                //for (int index2 = 0; index2 < this.savedata.chipFolder.GetLength(1); ++index2)
                // {
                //     var chipno = Random.Next(0, 150);
                //     this.savedata.chipFolder[index1, index2, 0] = chipno;
                //     var code = Random.Next(0, 3);
                //     this.savedata.chipFolder[index1, index2, 1] = code;

                // }

                this.main.FolderLoad();
            }

        }

        public void FieldSet(string name, Point posi, int floor, MapCharacterBase.ANGLE angle)
        {
            this.persistentEvents.Clear();
            this.field = new MapField(this.sound, name, this.savedata, this);
            this.player.PositionSet(posi, floor, angle);
            this.player.position.Z = this.savedata.pluginZ;
            this.player.field = this.field;
        }

        public override void Updata()
        {
            if (!this.DebugOn)
            {
                if (this.outoCamera)
                    this.CameraMove();
                this.Player.hitEvent = -1;
                this.Player.hitPlug = -1;
                if (this.mailflag)
                    this.MailAnime();
                if (this.ShakeFlag)
                    this.Shaking();
                if (this.fadeflug)
                    this.Fadeing();
                if (this.eventmanager.playevent)
                    this.eventmanager.UpDate();
                else if (this.eventmanagerParallel.playevent && !this.player.openMenu)
                    this.eventmanagerParallel.UpDate();
                if (!this.player.openMenu && !this.eventmanager.playevent)
                    this.TimerUpdate();
                // Temporary copy made in case a persistent event (runevent) adds another persistent event, will need to start next tick
                foreach (var persistentEvent in this.persistentEvents.ToArray())
                {
                    if (persistentEvent.IsActive)
                    {
                        persistentEvent.PersistentUpdate();
                    }
                }
                this.persistentEvents.RemoveAll(pe => !pe.IsActive);
                this.MapUpdate();
                this.HP.HPDown(this.savedata.HPNow, this.savedata.HPMax);
                this.field.Update();
            }
            else
                this.debugmode.Update();
        }

        private void TimerUpdate()
        {
            if (!this.savedata.FlagList[4] || this.savedata.FlagList[69])
                return;
            if (this.savedata.ValList[40] < this.savedata.ValList[41])
            {
                ++this.savedata.ValList[40];
                if (this.savedata.ValList[40] % 100 >= 60)
                    this.savedata.ValList[40] += 40;
                if (this.savedata.ValList[40] % 10000 >= 6000)
                    this.savedata.ValList[40] += 4000;
            }
            else if (this.savedata.ValList[40] > this.savedata.ValList[41])
            {
                int num1 = this.savedata.ValList[40] / 10000;
                int num2 = this.savedata.ValList[40] % 10000 / 100;
                int num3 = this.savedata.ValList[40] % 100 - 1;
                if (num3 < 0)
                {
                    num3 = 59;
                    --num2;
                }
                if (num2 < 0)
                {
                    num2 = 59;
                    --num1;
                }
                this.savedata.ValList[40] = num1 * 10000 + num2 * 100 + num3;
            }
            else if (this.savedata.ValList[40] == this.savedata.ValList[41])
            {
                this.sound.PlaySE(SoundEffect.rockon);
                this.savedata.FlagList[69] = true;
            }
        }

        private void MapUpdate()
        {
            this.player.Update();
        }

        public bool CanMove(MapCharacterBase.ANGLE angle, NSMap.Character.Player player, int floor)
        {
            Vector3 position = player.Position;
            player.overStep = false;
            bool flag1 = true;
            if (this.step == SceneMap.STEPS.normal)
            {
                int num1 = 0;
                int num2 = 0;
                int num3 = 3;
                switch (angle)
                {
                    case MapCharacterBase.ANGLE.DOWN:
                        num1 += num3;
                        num2 += num3;
                        break;
                    case MapCharacterBase.ANGLE.DOWNRIGHT:
                        num1 += num3;
                        break;
                    case MapCharacterBase.ANGLE.RIGHT:
                        num1 += num3;
                        num2 += -num3;
                        break;
                    case MapCharacterBase.ANGLE.UPRIGHT:
                        num2 += -num3;
                        break;
                    case MapCharacterBase.ANGLE.UP:
                        num1 += -num3;
                        num2 += -num3;
                        break;
                    case MapCharacterBase.ANGLE.UPLEFT:
                        num1 += -num3;
                        break;
                    case MapCharacterBase.ANGLE.LEFT:
                        num1 += -num3;
                        num2 += num3;
                        break;
                    case MapCharacterBase.ANGLE.DOWNLEFT:
                        num2 += num3;
                        break;
                }
                Point point1 = new Point((int)position.X / 8, (int)position.Y / 8);
                Point point2 = new Point((int)position.X % 8 + num1, (int)position.Y % 8 + num2);
                bool flag2 = false;
                if (point2.X < 0)
                {
                    if (point1.X - 1 < 0)
                        flag1 = false;
                    else if (this.field.Map_[floor, point1.X - 1, point1.Y] == 0 || this.field.Map_[floor, point1.X - 1, point1.Y] == 3 || this.field.Map_[floor, point1.X - 1, point1.Y] == 5 || this.field.Map_[floor, point1.X - 1, point1.Y] >= 6 && this.field.Map_[floor, point1.X - 1, point1.Y] <= 9)
                    {
                        flag1 = false;
                        if (this.field.Map_[floor, point1.X, point1.Y] == 2 && this.field.Map_[floor, point1.X - 1, point1.Y] == 6 && angle == MapCharacterBase.ANGLE.UP)
                            flag1 = true;
                        if (this.field.Map_[floor, point1.X, point1.Y] == 4 && this.field.Map_[floor, point1.X - 1, point1.Y] == 8 && angle == MapCharacterBase.ANGLE.LEFT)
                            flag1 = true;
                    }
                    flag2 = true;
                }
                else if (point2.X >= 8)
                {
                    if (point1.X + 1 >= this.field.Map_.GetLength(1) || this.field.Map_[floor, point1.X + 1, point1.Y] == 0 || (this.field.Map_[floor, point1.X + 1, point1.Y] == 2 || this.field.Map_[floor, point1.X + 1, point1.Y] == 4) || this.field.Map_[floor, point1.X + 1, point1.Y] >= 6 && this.field.Map_[floor, point1.X + 1, point1.Y] <= 9)
                        flag1 = false;
                    if (this.field.Map_[floor, point1.X, point1.Y] == 3 && this.field.Map_[floor, point1.X + 1, point1.Y] == 7 && angle == MapCharacterBase.ANGLE.DOWN)
                        flag1 = true;
                    if (this.field.Map_[floor, point1.X, point1.Y] == 5 && this.field.Map_[floor, point1.X + 1, point1.Y] == 9 && angle == MapCharacterBase.ANGLE.RIGHT)
                        flag1 = true;
                    flag2 = true;
                }
                if (point2.Y < 0)
                {
                    if (point1.Y - 1 < 0)
                        flag1 = false;
                    else if (this.field.Map_[floor, point1.X, point1.Y - 1] == 0 || this.field.Map_[floor, point1.X, point1.Y - 1] == 2 || this.field.Map_[floor, point1.X, point1.Y - 1] == 5 || this.field.Map_[floor, point1.X, point1.Y - 1] >= 6 && this.field.Map_[floor, point1.X, point1.Y - 1] <= 9)
                    {
                        flag1 = false;
                        if (this.field.Map_[floor, point1.X, point1.Y] == 3 && this.field.Map_[floor, point1.X, point1.Y - 1] == 7 && angle == MapCharacterBase.ANGLE.UP)
                            flag1 = true;
                        if (this.field.Map_[floor, point1.X, point1.Y] == 4 && this.field.Map_[floor, point1.X, point1.Y - 1] == 8 && angle == MapCharacterBase.ANGLE.RIGHT)
                            flag1 = true;
                    }
                    flag2 = true;
                }
                else if (point2.Y >= 8)
                {
                    if (point1.Y + 1 >= this.field.Map_.GetLength(2) || this.field.Map_[floor, point1.X, point1.Y + 1] == 0 || (this.field.Map_[floor, point1.X, point1.Y + 1] == 3 || this.field.Map_[floor, point1.X, point1.Y + 1] == 4) || this.field.Map_[floor, point1.X, point1.Y + 1] >= 6 && this.field.Map_[floor, point1.X, point1.Y + 1] <= 9)
                        flag1 = false;
                    if (this.field.Map_[floor, point1.X, point1.Y] == 2 && this.field.Map_[floor, point1.X, point1.Y + 1] == 6 && angle == MapCharacterBase.ANGLE.DOWN)
                        flag1 = true;
                    if (this.field.Map_[floor, point1.X, point1.Y] == 5 && this.field.Map_[floor, point1.X, point1.Y + 1] == 9 && angle == MapCharacterBase.ANGLE.LEFT)
                        flag1 = true;
                    flag2 = true;
                }
                if (!flag2)
                {
                    switch (this.field.Map_[floor, point1.X, point1.Y])
                    {
                        case 0:
                            flag1 = false;
                            break;
                        case 2:
                            if (point2.X * point2.Y < point2.Y * point2.Y)
                            {
                                flag1 = false;
                                break;
                            }
                            break;
                        case 3:
                            if (point2.X * point2.Y < point2.X * point2.X)
                            {
                                flag1 = false;
                                break;
                            }
                            break;
                        case 4:
                            if (point2.X * (7 - point2.Y) < (7 - point2.Y) * (7 - point2.Y))
                            {
                                flag1 = false;
                                break;
                            }
                            break;
                        case 5:
                            if (point2.X * (7 - point2.Y) < point2.X * point2.X)
                            {
                                flag1 = false;
                                break;
                            }
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            flag1 = false;
                            break;
                    }
                }
                if (flag1)
                {
                    Point point3 = new Point((int)player.position.X + num1, (int)player.position.Y + num2);
                    int num4 = -1;
                    foreach (MapEventBase mapEventBase in this.field.Events)
                    {
                        ++num4;
                        if (mapEventBase.LunPage.hitform
                            && mapEventBase.floor == player.floor
                            && mapEventBase.rendType == 1
                            && (point3.X >= mapEventBase.position.X + (double)mapEventBase.LunPage.hitShift.X - mapEventBase.LunPage.hitrange.X / 2
                            && point3.X <= mapEventBase.position.X + (double)mapEventBase.LunPage.hitShift.X + mapEventBase.LunPage.hitrange.X / 2
                            && point3.Y >= mapEventBase.position.Y + (double)mapEventBase.LunPage.hitShift.Y - mapEventBase.LunPage.hitrange.Y / 2
                            && point3.Y <= mapEventBase.position.Y + (double)mapEventBase.LunPage.hitShift.Y + mapEventBase.LunPage.hitrange.Y / 2))
                        {
                            flag1 = false;
                            if (mapEventBase.LunPage.startterms == EventPage.STARTTERMS.Touch)
                                player.hitEvent = num4;
                        }
                    }
                }
                int num5 = this.field.Map_[floor, point1.X, point1.Y];
                int num6 = 1;
                switch (num5)
                {
                    case 10:
                    case 12:
                        this.step = SceneMap.STEPS.rightstep;
                        player.stepCounter = num5 != 10 ? this.field.Height / 2 : 0.0f;
                        Point point4 = new Point(point1.X, point1.Y);
                        int num7;
                        do
                        {
                            --point4.Y;
                            num7 = this.field.Map_[floor, point4.X, point4.Y];
                            if (num7 == num5)
                            {
                                ++num6;
                            }
                            else
                            {
                                ++point4.Y;
                                this.stepover[0] = point4.Y * 8;
                            }
                        }
                        while (num7 == num5);
                        point4 = new Point(point1.X, point1.Y);
                        int num8;
                        do
                        {
                            ++point4.Y;
                            num8 = this.field.Map_[floor, point4.X, point4.Y];
                            if (num8 == num5)
                            {
                                ++num6;
                            }
                            else
                            {
                                this.stepover[1] = this.stepover[0] + num6 * 8;
                                this.stepover[0] += 3;
                                this.stepover[1] -= 3;
                            }
                        }
                        while (num8 == num5);
                        break;
                    case 11:
                    case 13:
                        this.step = SceneMap.STEPS.leftstepp;
                        player.stepCounter = num5 != 11 ? this.field.Height / 2 : 0.0f;
                        Point point5 = new Point(point1.X, point1.Y);
                        int num9;
                        do
                        {
                            --point5.X;
                            num9 = this.field.Map_[floor, point5.X, point5.Y];
                            if (num9 == num5)
                            {
                                ++num6;
                            }
                            else
                            {
                                ++point5.X;
                                this.stepover[0] = point5.X * 8;
                            }
                        }
                        while (num9 == num5);
                        point5 = new Point(point1.X, point1.Y);
                        int num10;
                        do
                        {
                            ++point5.X;
                            num10 = this.field.Map_[floor, point5.X, point5.Y];
                            if (num10 == num5)
                            {
                                ++num6;
                            }
                            else
                            {
                                this.stepover[1] = this.stepover[0] + num6 * 8;
                                this.stepover[0] += 3;
                                this.stepover[1] -= 3;
                            }
                        }
                        while (num10 == num5);
                        break;
                    default:
                        this.step = SceneMap.STEPS.normal;
                        break;
                }
                if (this.field.Height > 1)
                    player.floor = (int)player.Position.Z / (this.field.Height / 2);
                else
                    player.floor = 0;
            }
            else
            {
                this.StepOut();
                int num1 = (int)player.Position.Z / (this.field.Height / 2 - 1);
                int num2 = 0;
                int num3 = 0;
                int num4 = 1;
                switch (angle)
                {
                    case MapCharacterBase.ANGLE.DOWN:
                        num2 += num4;
                        num3 += num4;
                        break;
                    case MapCharacterBase.ANGLE.DOWNRIGHT:
                        num2 += num4;
                        break;
                    case MapCharacterBase.ANGLE.RIGHT:
                        num2 += num4;
                        num3 += -num4;
                        break;
                    case MapCharacterBase.ANGLE.UPRIGHT:
                        num3 += -num4;
                        break;
                    case MapCharacterBase.ANGLE.UP:
                        num2 += -num4;
                        num3 += -num4;
                        break;
                    case MapCharacterBase.ANGLE.UPLEFT:
                        num2 += -num4;
                        break;
                    case MapCharacterBase.ANGLE.LEFT:
                        num2 += -num4;
                        num3 += num4;
                        break;
                    case MapCharacterBase.ANGLE.DOWNLEFT:
                        num3 += num4;
                        break;
                }
                Point point = new Point((int)(position.X + (double)num2) / 8, (int)(position.Y + (double)num3) / 8);
                player.floor = num1;
                if (player.stepCounter < 0.0 || player.stepCounter > (double)(this.field.Height / 2))
                {
                    if ((uint)player.floor > 0U)
                        player.position.Z = this.field.Height / 2 * player.floor;
                    else
                        player.position.Z = 0.0f;
                    this.step = SceneMap.STEPS.normal;
                }
            }
            return flag1;
        }

        public bool CanMove_EventHit(MapCharacterBase.ANGLE a)
        {
            this.player.stopping = false;
            bool flag1 = true;
            Vector2 vector2 = new Vector2();
            switch (a)
            {
                case MapCharacterBase.ANGLE.DOWN:
                    vector2.X += this.player.Speed;
                    vector2.Y += this.player.Speed;
                    break;
                case MapCharacterBase.ANGLE.DOWNRIGHT:
                    vector2.X += this.player.Speed;
                    break;
                case MapCharacterBase.ANGLE.RIGHT:
                    vector2.X += this.player.Speed / 2f;
                    vector2.Y -= this.player.Speed / 2f;
                    break;
                case MapCharacterBase.ANGLE.UPRIGHT:
                    vector2.Y -= this.player.Speed;
                    break;
                case MapCharacterBase.ANGLE.UP:
                    vector2.X -= this.player.Speed;
                    vector2.Y -= this.player.Speed;
                    break;
                case MapCharacterBase.ANGLE.UPLEFT:
                    vector2.X -= this.player.Speed;
                    break;
                case MapCharacterBase.ANGLE.LEFT:
                    vector2.X -= this.player.Speed / 2f;
                    vector2.Y += this.player.Speed / 2f;
                    break;
                case MapCharacterBase.ANGLE.DOWNLEFT:
                    vector2.Y += this.player.Speed;
                    break;
            }
            int num1 = -1;
            foreach (MapEventBase mapEventBase in this.field.Events)
            {
                ++num1;
                if (mapEventBase.floor == this.player.floor && (mapEventBase.LunPage.hitrange.X != 0 || mapEventBase.LunPage.hitrange.Y != 0))
                {
                    bool isColliding = false;
                    Vector3 vector3_1 = new Vector3();
                    vector3_1 = !mapEventBase.LunPage.character ? this.player.position : this.player.Position;
                    vector3_1.X += vector2.X;
                    vector3_1.Y += vector2.Y;
                    // circle collision check (walkthrough, handling later)
                    if (!mapEventBase.LunPage.hitform)
                    {
                        float num2 = MyMath.Pow(mapEventBase.position.X + mapEventBase.LunPage.hitShift.X - vector3_1.X, 2)
                                   + MyMath.Pow(mapEventBase.position.Y + mapEventBase.LunPage.hitShift.Y - vector3_1.Y, 2);
                        isColliding = MyMath.Pow(mapEventBase.LunPage.startterms == EventPage.STARTTERMS.Touch
                                            ? mapEventBase.LunPage.hitrange.X + 2
                                            : mapEventBase.LunPage.hitrange.X + (a != MapCharacterBase.ANGLE.none
                                                                                    ? this.player.hitLange
                                                                                    : this.player.hitLange + 1),
                                            2)
                                >= (double)num2;
                        // disable collision for 0-size circles
                        isColliding &= mapEventBase.LunPage.hitrange.X != 0;
                    }
                    else if (mapEventBase.LunPage.startterms == EventPage.STARTTERMS.Abutton || mapEventBase.LunPage.startterms == EventPage.STARTTERMS.Rbutton)
                    {
                        if ((uint)mapEventBase.LunPage.move.Length > 0U)
                        {
                            int num2 = 0;
                            int num3 = 0;
                            int num4 = 0;
                            int num5 = 0;
                            switch (mapEventBase.LunPage.angle)
                            {
                                case MapCharacterBase.ANGLE.DOWNRIGHT:
                                    num2 = 16;
                                    break;
                                case MapCharacterBase.ANGLE.UPRIGHT:
                                    num5 = 16;
                                    break;
                                case MapCharacterBase.ANGLE.UPLEFT:
                                    num3 = 16;
                                    break;
                                case MapCharacterBase.ANGLE.DOWNLEFT:
                                    num4 = 16;
                                    break;
                            }
                            int num6 = this.player.hitLange + 3;
                            isColliding = vector3_1.X >= mapEventBase.position.X + (double)mapEventBase.LunPage.hitShift.X - mapEventBase.LunPage.hitrange.X / 2 - num6 - num3
                                && vector3_1.X <= mapEventBase.position.X + (double)mapEventBase.LunPage.hitShift.X + mapEventBase.LunPage.hitrange.X / 2 + num6 + num2
                                && vector3_1.Y >= mapEventBase.position.Y + (double)mapEventBase.LunPage.hitShift.Y - mapEventBase.LunPage.hitrange.Y / 2 - num6 - num5
                                && vector3_1.Y <= mapEventBase.position.Y + (double)mapEventBase.LunPage.hitShift.Y + mapEventBase.LunPage.hitrange.Y / 2 + num6 + num4;
                        }
                        else
                        {
                            int num2 = this.player.hitLange + 3;
                            isColliding = vector3_1.X >= mapEventBase.position.X + (double)mapEventBase.LunPage.hitShift.X - mapEventBase.LunPage.hitrange.X / 2 - num2
                                && vector3_1.X <= mapEventBase.position.X + (double)mapEventBase.LunPage.hitShift.X + mapEventBase.LunPage.hitrange.X / 2 + num2
                                && vector3_1.Y >= mapEventBase.position.Y + (double)mapEventBase.LunPage.hitShift.Y - mapEventBase.LunPage.hitrange.Y / 2 - num2
                                && vector3_1.Y <= mapEventBase.position.Y + (double)mapEventBase.LunPage.hitShift.Y + mapEventBase.LunPage.hitrange.Y / 2 + num2;
                        }
                    }
                    if (isColliding && mapEventBase.LunPage.NormalMan)
                        this.player.stopping = true;
                    if (isColliding && (this.player.stoptime <= 120 || !mapEventBase.LunPage.NormalMan))
                    {
                        mapEventBase.playeHit = true;
                        if (mapEventBase.LunPage.startterms == EventPage.STARTTERMS.Touch)
                            this.player.hitEvent = num1;
                        this.player.canmove = new bool[Enum.GetNames(typeof(MapCharacterBase.ANGLE)).Length];
                        if (mapEventBase.LunPage.hitform)
                        {
                            float num2 = mapEventBase.position.Y + mapEventBase.LunPage.hitShift.Y - mapEventBase.LunPage.hitrange.Y / 2;
                            float num3 = mapEventBase.position.Y + mapEventBase.LunPage.hitShift.Y + mapEventBase.LunPage.hitrange.Y / 2;
                            float num4 = mapEventBase.position.X + mapEventBase.LunPage.hitShift.X - mapEventBase.LunPage.hitrange.X / 2;
                            float num5 = mapEventBase.position.X + mapEventBase.LunPage.hitShift.X + mapEventBase.LunPage.hitrange.X / 2;
                            Vector3 vector3_2 = vector3_1;
                            if (vector3_2.Y < (double)num2)
                            {
                                if (vector3_2.X < (double)num4)
                                    this.player.canmove[0] = true;
                                else if (vector3_2.X >= (double)num4 && vector3_2.X <= (double)num5)
                                {
                                    this.player.canmove[0] = true;
                                    this.player.canmove[7] = true;
                                    this.player.canmove[6] = true;
                                }
                                else if (vector3_2.X > (double)num5)
                                    this.player.canmove[6] = true;
                            }
                            else if (vector3_2.Y >= (double)num2 && vector3_2.Y <= (double)num3)
                            {
                                if (vector3_2.X < (double)num4)
                                {
                                    this.player.canmove[1] = true;
                                    this.player.canmove[2] = true;
                                    this.player.canmove[0] = true;
                                }
                                else
                                {
                                    this.player.canmove[5] = true;
                                    this.player.canmove[6] = true;
                                    this.player.canmove[4] = true;
                                }
                            }
                            else if (vector3_2.X < (double)num4)
                                this.player.canmove[2] = true;
                            else if (vector3_2.X >= (double)num4 && vector3_2.X <= (double)num5)
                            {
                                this.player.canmove[3] = true;
                                this.player.canmove[4] = true;
                                this.player.canmove[2] = true;
                            }
                            else if (vector3_2.X > (double)num5)
                                this.player.canmove[4] = true;
                        }
                        else
                        {
                            if (mapEventBase.LunPage.NormalMan)
                            {
                                if (!this.eventmanager.playevent && !this.player.openMenu)
                                {
                                    this.player.stopping = true;
                                    ++this.player.stoptime;
                                }
                                else
                                    this.player.stoptime = 0;
                            }
                            float num2 = mapEventBase.position.Y + mapEventBase.LunPage.hitShift.Y - mapEventBase.LunPage.hitrange.X;
                            float num3 = mapEventBase.position.Y + mapEventBase.LunPage.hitShift.Y + mapEventBase.LunPage.hitrange.X;
                            float num4 = mapEventBase.position.X + mapEventBase.LunPage.hitShift.X - mapEventBase.LunPage.hitrange.X;
                            float num5 = mapEventBase.position.X + mapEventBase.LunPage.hitShift.X + mapEventBase.LunPage.hitrange.X;
                            Vector3 vector3_2 = vector3_1;
                            if (vector3_2.Y < (double)num2)
                            {
                                if (vector3_2.X < (double)num4)
                                    this.player.canmove[0] = true;
                                else if (vector3_2.X >= (double)num4 && vector3_2.X <= (double)num5)
                                {
                                    this.player.canmove[0] = true;
                                    this.player.canmove[7] = true;
                                    this.player.canmove[6] = true;
                                }
                                else if (vector3_2.X > (double)num5)
                                    this.player.canmove[6] = true;
                            }
                            else if (vector3_2.Y >= (double)num2 && vector3_2.Y <= (double)num3)
                            {
                                if (vector3_2.X < (double)num4)
                                {
                                    this.player.canmove[1] = true;
                                    this.player.canmove[2] = true;
                                    this.player.canmove[0] = true;
                                }
                                else
                                {
                                    this.player.canmove[5] = true;
                                    this.player.canmove[6] = true;
                                    this.player.canmove[4] = true;
                                }
                            }
                            else if (vector3_2.X < (double)num4)
                                this.player.canmove[2] = true;
                            else if (vector3_2.X >= (double)num4 && vector3_2.X <= (double)num5)
                            {
                                this.player.canmove[3] = true;
                                this.player.canmove[4] = true;
                                this.player.canmove[2] = true;
                            }
                            else if (vector3_2.X > (double)num5)
                                this.player.canmove[4] = true;
                        }
                        if (this.player.canmove[(int)this.player.Angle])
                        {
                            if (mapEventBase.LunPage.startterms == EventPage.STARTTERMS.Rbutton)
                                this.player.hitPlug = num1;
                            else
                                this.player.hitEvent = num1;
                        }
                        if (flag1)
                            flag1 = mapEventBase.LunPage.hitform && mapEventBase.LunPage.rendType <= 1;
                    }
                    else
                        mapEventBase.playeHit = false;
                }
            }
            if (!this.player.stopping)
                this.player.stoptime = 0;
            return flag1;
        }

        public void StepOut()
        {
            switch (this.step)
            {
                case SceneMap.STEPS.rightstep:
                    if (player.Position.Y < (double)this.stepover[0])
                    {
                        this.player.overStep = true;
                        this.player.position.Y = this.stepover[0] + 8;
                    }
                    if (player.Position.Y <= (double)this.stepover[1])
                        break;
                    this.player.overStep = true;
                    this.player.position.Y = this.stepover[1] + 8;
                    break;
                case SceneMap.STEPS.leftstepp:
                    if (player.Position.X < (double)this.stepover[0])
                    {
                        this.player.overStep = true;
                        this.player.position.X = this.stepover[0] + 8;
                    }
                    if (player.Position.X <= (double)this.stepover[1])
                        break;
                    this.player.overStep = true;
                    this.player.position.X = this.stepover[1] + 8;
                    break;
            }
        }

        private void CameraMove()
        {
            float num1 = !this.setCameraOn ? this.player.Position.X : this.setCamera.X;
            float num2 = !this.setCameraOn ? this.player.Position.Y : this.setCamera.Y;
            float num3 = !this.setCameraOn ? this.player.Position.Z : this.setCamera.Z;
            this.field.camera.X = (int)(this.field.MapsizeX / 2 + 2.0 * num1 - num2 * 2.0 + cameraPlus.X);
            this.field.camera.Y = (int)(num1 + (double)num2 + num3 - 4.0 + cameraPlus.Y) - 8f;
        }

        public override void Render(IRenderer dg)
        {
            this.field.Render(dg);
            if (this.player.openMenu)
            {
                if (alpha > 0.0)
                {
                    Color color = Color.FromArgb((int)this.alpha, this.fadeColor);
                    this._rect = new Rectangle(0, 0, 240, 160);
                    this._position = new Vector2(0.0f, 0.0f);
                    dg.DrawImage(dg, "fadescreen", this._rect, true, this._position, color);
                }
                this.player.menu.Render(dg);
            }
            else
            {
                if (!this.hideStatus)
                {
                    if (this.savedata.isJackedIn)
                    {
                        Vector2 vector2 = new Vector2(24f, 8f);
                        this._rect = new Rectangle(80, 0, 44, 16);
                        this._position = vector2;
                        dg.DrawImage(dg, "battleobjects", this._rect, false, this._position, Color.White);
                        this.HP.HPRender(dg, new Vector2(vector2.X + 12f, vector2.Y - 1f));
                        // 38: ROM element
                        if (this.savedata.ValList[38] > 0)
                        {
                            if (this.savedata.ValList[38] == 8 || this.savedata.ValList[38] == 7)
                            {
                                this._rect = new Rectangle(216, 88, 16, 16);
                                this._position = new Vector2(48f, 0.0f);
                                dg.DrawImage(dg, "battleobjects", this._rect, true, this._position, Color.White);
                            }
                            else
                            {
                                this._rect = new Rectangle(216 + this.savedata.ValList[38] * 16, 88, 16, 16);
                                this._position = new Vector2(48f, 0.0f);
                                dg.DrawImage(dg, "battleobjects", this._rect, true, this._position, Color.White);
                            }
                        }
                    }
                    else
                    {
                        this._rect = new Rectangle(208 + phoneAnime % 2 * 16, 264, 16, 24);
                        this._position = new Vector2(0.0f, 0.0f);
                        dg.DrawImage(dg, "menuwindows", this._rect, true, this._position, Color.White);
                        if (this.phoneAnime > 0)
                        {
                            this._rect = new Rectangle(240 + phoneAnime * 16, 264, 16, 16);
                            this._position = new Vector2(16f, 0.0f);
                            dg.DrawImage(dg, "menuwindows", this._rect, true, this._position, Color.White);
                        }
                        if (!this.savedata.FlagList[0])
                        {
                            this._rect = new Rectangle(240, 264, 16, 16);
                            this._position = new Vector2(16f, 8f);
                            dg.DrawImage(dg, "menuwindows", this._rect, true, this._position, Color.White);
                        }
                    }
                    Vector2 vector2_1 = new Vector2(240 - this.savedata.plase.Length * 8, 146f)
                    {
                        X = 2f
                    };
                    this._position = new Vector2(vector2_1.X - 1f, vector2_1.Y - 1f);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = new Vector2(vector2_1.X - 1f, vector2_1.Y);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = new Vector2(vector2_1.X - 1f, vector2_1.Y + 1f);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = new Vector2(vector2_1.X, vector2_1.Y - 1f);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = new Vector2(vector2_1.X, vector2_1.Y + 1f);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = new Vector2(vector2_1.X + 1f, vector2_1.Y - 1f);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = new Vector2(vector2_1.X + 1f, vector2_1.Y);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = new Vector2(vector2_1.X + 1f, vector2_1.Y + 1f);
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.FromArgb(32, 32, 32));
                    this._position = vector2_1;
                    dg.DrawMicroText(this.savedata.plase, this._position, Color.White);
                    if (this.savedata.FlagList[4] || this.savedata.FlagList[69])
                    {
                        string str1 = (this.savedata.ValList[40] / 10000).ToString();
                        if (str1.Length < 2)
                            str1 = "0" + str1;
                        int num = this.savedata.ValList[40] % 10000 / 100;
                        string str2 = num.ToString();
                        if (str2.Length < 2)
                            str2 = "0" + str2;
                        num = this.savedata.ValList[40] % 100;
                        string str3 = num.ToString();
                        if (str3.Length < 2)
                            str3 = "0" + str3;
                        string txt = str1 + "：" + str2 + "：" + str3;
                        Color color = Color.White;
                        if (this.savedata.ValList[40] <= 300)
                            color = Color.Orange;
                        else if (this.savedata.ValList[40] <= 500)
                            color = Color.Yellow;
                        if (this.savedata.ValList[40] <= 1000 && this.savedata.ValList[40] % 100 == 0 && (!this.savedata.FlagList[69] && this.savedata.ValList[40] != this.savedata.ValList[41]) && !this.eventmanager.playevent)
                            this.sound.PlaySE(SoundEffect.search);
                        this.TextRender(dg, txt, false, new Vector2(176f, 0.0f), false, color);
                    }
                }
                if (alpha > 0.0)
                {
                    if (alpha > (double)byte.MaxValue)
                        this.alpha = byte.MaxValue;
                    Color color = Color.FromArgb((int)this.alpha, this.fadeColor);
                    this._rect = new Rectangle(0, 0, 240, 160);
                    this._position = new Vector2(0.0f, 0.0f);
                    dg.DrawImage(dg, "fadescreen", this._rect, true, this._position, color);
                }
            }
            if (this.eventmanager.playevent)
            {
                this.eventmanager.Render(dg);
            }
            if (this.eventmanagerParallel.playevent)
            {
                this.eventmanagerParallel.Render(dg);
            }
            // Temporary copy made in case of race condition modifying collection
            foreach (var persistentEvent in this.persistentEvents.ToArray())
            {
                if (persistentEvent.IsActive)
                {
                    persistentEvent.PersistentRender(dg);
                }
            }
            if (this.DebugOn)
            {
                this.debugmode.Render(dg);
            }
        }

        private void MailAnime()
        {
            if (this.mailflame % 4 == 0)
            {
                ++this.phoneAnime;
                if (this.phoneAnime >= 4)
                    this.phoneAnime = 0;
            }
            switch (this.mailflame)
            {
                case 0:
                    if (this.mailsound)
                    {
                        this.sound.PlaySE(SoundEffect.mail);
                        break;
                    }
                    break;
                case 40:
                    this.mailflame = -1;
                    ++this.mailloop;
                    break;
            }
            ++this.mailflame;
            if (this.mailloop != 3)
                return;
            this.mailflag = false;
            this.mailloop = 0;
            this.mailflame = 0;
            this.phoneAnime = 0;
        }

        public void MailOn(bool sound)
        {
            this.mailloop = 0;
            this.mailflag = true;
            this.mailsound = sound;
        }

        public void FadeStart(Color fadecolor, int fadetime)
        {
            if (fadetime > 0)
            {
                this.R = fadeColor.R;
                this.G = fadeColor.G;
                this.B = fadeColor.B;
                this.plusAlpha = (fadecolor.A - this.alpha) / fadetime;
                this.plusR = (fadecolor.R - (float)this.fadeColor.R) / fadetime;
                this.plusG = (fadecolor.G - (float)this.fadeColor.G) / fadetime;
                this.plusB = (fadecolor.B - (float)this.fadeColor.B) / fadetime;
                this.fadeFlame = 0;
                this.fadeTime = fadetime;
                this.endAlpha = fadecolor.A;
                this.endR = fadecolor.R;
                this.endG = fadecolor.G;
                this.endB = fadecolor.B;
                if (alpha == 0.0)
                {
                    this.R = fadecolor.R;
                    this.G = fadecolor.G;
                    this.B = fadecolor.B;
                    this.plusR = 0.0f;
                    this.plusG = 0.0f;
                    this.plusB = 0.0f;
                }
                this.fadeflug = true;
            }
            else
            {
                this.alpha = fadecolor.A;
                this.R = fadecolor.R;
                this.G = fadecolor.G;
                this.B = fadecolor.B;
                this.endR = fadecolor.R;
                this.endG = fadecolor.G;
                this.endB = fadecolor.B;
                this.fadeColor = fadecolor;
                this.fadeflug = false;
            }
        }

        private void Fadeing()
        {
            if (this.fadeFlame >= this.fadeTime)
            {
                this.fadeflug = false;
                this.alpha = endAlpha;
                this.R = endR;
                this.G = endG;
                this.B = endB;
                this.fadeFlame = 0;
                if (alpha == 0.0)
                {
                    this.R = 0.0f;
                    this.G = 0.0f;
                    this.B = 0.0f;
                }
            }
            else
            {
                ++this.fadeFlame;
                this.alpha += this.plusAlpha;
                this.R += this.plusR;
                this.G += this.plusG;
                this.B += this.plusB;
                if (R > (double)byte.MaxValue)
                    this.R = byte.MaxValue;
                if (G > (double)byte.MaxValue)
                    this.G = byte.MaxValue;
                if (B > (double)byte.MaxValue)
                    this.B = byte.MaxValue;
                if (alpha > (double)byte.MaxValue)
                    this.alpha = byte.MaxValue;
            }
            this.fadeColor = Color.FromArgb((int)this.R, (int)this.G, (int)this.B);
        }

        public enum STEPS
        {
            normal,
            rightstep,
            leftstepp,
        }
    }
}
