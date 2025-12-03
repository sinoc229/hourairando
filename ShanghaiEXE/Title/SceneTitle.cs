using NSShanghaiEXE.InputOutput;
using NSShanghaiEXE.InputOutput.Audio;
using NSShanghaiEXE.InputOutput.Rendering;
using NSGame;
using Common.Vectors;
using System.Drawing;
using NSMap.Character.Menu;
using System.Linq;
using System;

namespace NSTitle
{
    internal class SceneTitle : SceneBase
    {
        private readonly string titlemusic = "title";
        private int keywait = 0;
        private int fadealpha = byte.MaxValue;
        private byte backpx = 0;

        private Vector2 fontposition = new Vector2(120f, 128f);
        private SceneTitle.TITLEMENU menu = SceneTitle.TITLEMENU.Start;
        private bool printpush = true;
        private bool[] star = new bool[10];
        private int plus;
        private readonly bool test;
        private const byte fadespeed = 8;
        private const byte backspeed = 3;
        private SceneTitle.TITLESCENE nowscene;
        private bool printLoad;
        private int stars;
        private int starAnime;

        private int menuoffy = 64;
        private int menuoffx = 30;
        private int optionsel = 2;
        private int optionmax = 4;
        private int hassavefile = 1;
        private bool isinfade = false;
        private bool starforcemode = false;
        private bool randomfolder = false;

        private byte linescrl1 = 0;
        private int linescrl2 = 0;
        private int linescrl3 = 0;
        private int topoff = 30;
        private int fadealp = 0;

        private SceneTitle.TITLEMENU Menu
        {
            get
            {
                return this.menu;
            }
            set
            {
                this.menu = value;
                if (this.menu < SceneTitle.TITLEMENU.Start)
                    this.menu = SceneTitle.TITLEMENU.Load;
                if (this.menu <= SceneTitle.TITLEMENU.Load)
                    return;
                this.menu = SceneTitle.TITLEMENU.Start;
            }
        }

        public SceneTitle(IAudioEngine s, ShanghaiEXE p, SaveData save)
          : base(s, p, save)
        {
            this.parent = p;
            this.nowscene = SceneTitle.TITLESCENE.init;
            this.test = false;
           // if (this.test == false) this check is buggy and weird
           // {
           //     optionsel = 0;
           //     hassavefile = 0;
           // }
            this.printLoad = p.loadSUCCESS;
        }

        private void StarCheck()
        {
            this.star = new bool[10];
            this.stars = 0;
            // Game end flag
            if (this.savedata.FlagList[788])
            {
                this.star[0] = true;
                this.stars++;
            }
            if (this.savedata.FlagList[1701])
            {
                this.star[1] = true;
                this.stars++;
            }

            var completionLibrary = new Library(this.sound, null, null, this.savedata);
            if (completionLibrary.LibraryPages[Library.LibraryPageType.Normal].Chips.All(c => c.IsSeen))
            {
                this.star[2] = true;
                this.stars++;
            }
            if (completionLibrary.LibraryPages[Library.LibraryPageType.Navi].Chips.All(c => c.IsSeen))
            {
                this.star[3] = true;
                this.stars++;
            }
            if (completionLibrary.LibraryPages[Library.LibraryPageType.Dark].Chips.All(c => c.IsSeen))
            {
                this.star[4] = true;
                this.stars++;
            }
            if (completionLibrary.LibraryPages[Library.LibraryPageType.PA].Chips.All(c => c.IsSeen))
            {
                this.star[5] = true;
                this.stars++;
            }
            if (this.savedata.FlagList[1702])
            {
                this.star[6] = true;
                this.stars++;
            }

            int[] sflSP = { 620,621,622,623,625,626,
                            627,628,629,630,631,632,
                            633,634,635,636,640 };
            bool star7 = true;
            for (int flSP = 0; flSP < sflSP.Length; flSP++)
            {
                if (!this.savedata.FlagList[sflSP[flSP]])
                {
                    star7 = false;
                }
                //int a = 0;
            }

            if (star7)
            {
                this.star[7] = true;
                this.stars++;
            }

            /*if (this.savedata.FlagList[1703])
            {
                this.star[7] = true;
                this.stars++;
            }*/

            if (this.savedata.FlagList[1704])
            {
                this.star[8] = true;
                this.stars++;
            }
            if (this.savedata.FlagList[1705])
            {
                this.star[9] = true;
                this.stars++;
            }
        }

        public override void Updata()
        {
            switch (this.nowscene)
            {
                case SceneTitle.TITLESCENE.init:
                    if (this.fadealpha <= 0)
                    {
                        if (this.savedata.loadEnd)
                        {
                            this.printLoad = this.savedata.loadSucces;
                            if (this.printLoad)
                                this.menu = SceneTitle.TITLEMENU.Load;
                            this.nowscene = SceneTitle.TITLESCENE.pushbutton;
                            this.sound.StartBGM(this.titlemusic);
                            this.printpush = true;
                            break;
                        }
                        break;
                    }
                    this.fadealpha -= 8;
                    if (this.fadealpha <= 0)
                        this.fadealpha = 0;
                    break;
                case SceneTitle.TITLESCENE.pushbutton:
                    if (Input.IsPush(Button.Esc))
                        this.parent.Close();
                    if (backpx % 64 == 0)
                        this.printpush = this.savedata.loadEnd && !this.printpush;
                    if (Input.IsPress(Button._Start) && this.savedata.loadEnd)
                    {
                        this.StarCheck();
                        this.sound.PlaySE(SoundEffect.decide);
                        this.nowscene = SceneTitle.TITLESCENE.select;
                        this.frame = 0;
                        this.ShakeEnd();
                        break;
                    }
                    break;
                case SceneTitle.TITLESCENE.select:
                    if (Input.IsPush(Button.Esc))
                        this.parent.Close();
                    if (backpx % 8 == 0)
                    {
                        ++this.frame;
                        if (this.frame > 12)
                            ++this.starAnime;
                        if (this.starAnime >= 4)
                        {
                            this.starAnime = 0;
                            this.frame = 0;
                        }
                    }
                    this.KeyControl();
                    this.Command();
                    break;
                case SceneTitle.TITLESCENE.fade:
                    this.fadealpha += 8;
                    if (this.fadealpha >= byte.MaxValue)
                    {
                        this.fadealpha = byte.MaxValue;
                        this.parent.battlenum = 0;
                        //make menu buttons do stuff when fade is completed
                        switch (optionsel)
                        {
                            case 0:
                                if (this.savedata.loadEnd)
                                {
                                    this.parent.ChangeOfSecne(Scene.Main);
                                    this.parent.LoadGame_story(starforcemode,randomfolder);
                                    
                                    //start story mode
                                    break;
                                }
                                break;
                            case 1:
                                if (this.savedata.loadEnd)
                                {
                                    this.parent.ChangeOfSecne(Scene.Main);
                                    this.parent.LoadGame_freeroam(starforcemode,randomfolder);
                                    //start freeroam
                                    break;
                                }
                                break;
                            case 2:
                                if (this.savedata.loadEnd)
                                {
                                    if (this.printLoad)
                                    {
                                        this.parent.ChangeOfSecne(Scene.Main);
                                        this.parent.LoadGame();
                                        //load whatevs
                                    }
                                    else
                                    {
                                        this.parent.ChangeOfSecne(Scene.Main);
                                        this.parent.LoadGame_story(starforcemode, randomfolder);
                                        //this.parent.LoadGame();
                                        //fallack, start story mode if the user don't actually have a save
                                    }
                                    break;
                                }
                                break;
                        }
                        this.sound.StopBGM();
                        break;
                    }
                    break;
            }



            //scuffed paralaxing
            this.linescrl1 += 1;
            if (this.linescrl1 > 240)
            {
                this.linescrl1 = 0;
            }

            this.linescrl2 += 2;
            if (this.linescrl2 > 240)
            {
                this.linescrl2 -= 240;
            }

            this.linescrl3 += 3;
            if (this.linescrl3 > 240)
            {
                this.linescrl3 -= 240;
            }

            //why why why oh why isn't there a lerp 

            if (this.isinfade == true)
             {
                if (this.topoff > 0)
                {
                    topoff -= 5;
                }

                if (this.fadealp < 180)
                {
                    fadealp += 15;
                }
            }

            if (this.isinfade == false)
            {
                if (this.topoff < 30)
                {
                    topoff += 5;
                }

                if (this.fadealp > 0)
                {
                    fadealp -= 15;
                }
            }

            this.backpx += 3; //why did he code it like this
            if (this.backpx < 240)
                return;
            this.backpx = 0;

            


        }

        private void KeyControl()
        {
            if (Input.IsPress(Button._A) || Input.IsPress(Button._Start))
            {
                if (optionsel < 3)
                {
                    //for newgame and loading
                    this.nowscene = SceneTitle.TITLESCENE.fade;
                    this.sound.PlaySE(SoundEffect.thiptransmission);
                }
                else
                {
                    //for the option options
                    //this.sound.PlaySE(SoundEffect.thiptransmission);
                    switch (optionsel)
                    {
                        case 3:
                            starforcemode = !starforcemode;
                            this.sound.PlaySE(SoundEffect.thiptransmission);
                            break;
                        case 4:
                            randomfolder = !randomfolder;
                            this.sound.PlaySE(SoundEffect.thiptransmission);
                            break;
                    
                    }


                }
            }
            if (Input.IsPress(Button._B))
            {
                this.nowscene = SceneTitle.TITLESCENE.pushbutton;
                this.sound.PlaySE(SoundEffect.cancel);
            }
            if (this.keywait <= 0)
            {
                //if (!this.printLoad)
                //    return;
                if (Input.IsPush(Button.Up))
                {
                    --optionsel;
                    this.keywait = Input.IsPress(Button.Up) ? 25 : 5;
                    this.sound.PlaySE(SoundEffect.movecursol);
                }
                if (Input.IsPush(Button.Down))
                {
                    ++optionsel;
                    this.keywait = Input.IsPress(Button.Down) ? 25 : 5;
                    this.sound.PlaySE(SoundEffect.movecursol);
                }
            }
            else
                this.keywait = Input.IsUp(Button.Up) || Input.IsUp(Button.Down) ? 0 : this.keywait - 1;


            //you'd think there would be a wrap method built in somewhere, but no
            if (optionsel < 0)
            {
                optionsel = optionmax;
            }
            if (optionsel > optionmax)
            {
                optionsel = 0;
            }


        }

        private void Command()
        {
            if (!Input.IsPush(Button._Select))
                return;
            this.CommandInput("LR");
            if (this.CommandCheck("LRLLRLR"))
            {
                this.sound.PlaySE(SoundEffect.docking);
                this.plus = 1;
                this.CommandReset();
            }
            if (this.CommandCheck("RRLLRLL"))
            {
                this.sound.PlaySE(SoundEffect.docking);
                this.plus = 2;
                this.CommandReset();
            }
            if (this.CommandCheck("RRRLLLRL"))
            {
                this.sound.PlaySE(SoundEffect.docking);
                this.plus = 9;
                this.CommandReset();
            }
            if (this.CommandCheck("LLLRRRLR"))
            {
                this.sound.PlaySE(SoundEffect.docking);
                this.plus = -1;
                this.CommandReset();
            }
            if (this.CommandCheck("RLRLRRL"))
            {
                this.sound.PlaySE(SoundEffect.docking);
                this.plus = -2;
                this.CommandReset();
            }
        }

        public override void Render(IRenderer dg)
        {
            #region background blue thing
            this._rect = new Rectangle(0, 320, 240, 160);
            this._position = new Vector2(backpx, 0.0f);
            dg.DrawImage(dg, "title2", this._rect, true, this._position, false, Color.White);
            this._position = new Vector2(backpx - 240, 0.0f);
            dg.DrawImage(dg, "title2", this._rect, true, this._position, false, Color.White);
            #endregion

            #region gradient bg
            this._position = new Vector2(0, 0.0f);
            this._rect = new Rectangle(240, 160, 240, 160);
            dg.DrawImage(dg, "title2", this._rect, true, this._position, false, Color.White); // grid bg
            #endregion


            #region lines
            this._rect = new Rectangle(0, 0, 240, 292);
            this._position = new Vector2(linescrl3, 16);
            //dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            this._position = new Vector2((linescrl3 - 240), 16);
            //dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            #endregion

            #region small text
            this._rect = new Rectangle(0, 343, 240, 17); //146)
            //top
            this._position = new Vector2(linescrl2, 12);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            this._position = new Vector2((linescrl2 - 240), 12);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            //bot
            this._position = new Vector2(linescrl2, 160-30);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            this._position = new Vector2((linescrl2 - 240), 160-30);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            #endregion

            #region big text
            this._rect = new Rectangle(0, 490, 240, 42); //146)
            //top
            this._position = new Vector2(linescrl1, 0);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            this._position = new Vector2((linescrl1 - 240), 0);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            //bot
            this._position = new Vector2(linescrl1, 138-20);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            this._position = new Vector2((linescrl1 - 240), 138-20);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);
            #endregion

            var logoBorderSprite = ShanghaiEXE.languageTranslationService.GetLocalizedSprite("SceneTitle.LogoBorder");
            this._rect = logoBorderSprite.Item2;
            this._position = new Vector2(0.0f, 0.0f);
            dg.DrawImage(dg, logoBorderSprite.Item1, this._rect, true, this._position, false, Color.White);
            var logoTextSprite = ShanghaiEXE.languageTranslationService.GetLocalizedSprite("SceneTitle.LogoText");
            this._rect = logoTextSprite.Item2;
            this._position = new Vector2(0.0f, 0.0f);
            dg.DrawImage(dg, logoTextSprite.Item1, this._rect, true, this._position, false, Color.White);
            var logoCutoutSprite = ShanghaiEXE.languageTranslationService.GetLocalizedSprite("SceneTitle.LogoCutout");
            this._rect = logoCutoutSprite.Item2;
            this._position = new Vector2(0.0f, 0.0f);
            dg.DrawImage(dg, logoCutoutSprite.Item1, this._rect, true, this._position, false, Color.White);
            switch (this.nowscene)
            {
                case SceneTitle.TITLESCENE.pushbutton:
                    this.PushbuttonRender(dg);
                    this.isinfade = false;
                    break;
                case SceneTitle.TITLESCENE.select:
                case SceneTitle.TITLESCENE.fade:
                    this.FadeRender(dg);
                    this.isinfade = true;
                    break;
            }
            Color color = Color.FromArgb(this.fadealpha, 0, 0, 0);
            if (this.nowscene == SceneTitle.TITLESCENE.init)
                color = Color.FromArgb(this.fadealpha, Color.White);
            this._rect = new Rectangle(0, 0, 240, 160);
            this._position = new Vector2(0.0f, 0.0f);
            dg.DrawImage(dg, "fadescreen", this._rect, true, this._position, color);


            //darken stuff
            if (this.isinfade == false) //smooth fadeout
            {
                Color color1 = Color.FromArgb(fadealp, 0, 0, 0);
                this._rect = new Rectangle(0, 0, 240, 160);
                this._position = new Vector2(0.0f, 0.0f);
                dg.DrawImage(dg, "fadescreen", this._rect, true, this._position, false, color1);

                //top and bottom bit
                this._rect = new Rectangle(0, 292, 240, 20);
                this._position = new Vector2(0, 0 - topoff);
                dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);


                this._rect = new Rectangle(0, 323, 240, 20);
                this._position = new Vector2(0, 160 - 20 + topoff);
                dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);

            }
        }

        private void PushbuttonRender(IRenderer dg)
        {
            if (!this.printpush)
                return;
            this._rect = new Rectangle(248, 0, 128, 16);
            this._position = new Vector2(120f, 120f);
            dg.DrawImage(dg, "title", this._rect, false, this._position, false, Color.White);
        }

        private void FadeRender(IRenderer dg)
        {
            //darken stuff
            Color color1 = Color.FromArgb(fadealp, 0, 0, 0);
            this._rect = new Rectangle(0, 0, 240, 160);
            this._position = new Vector2(0.0f, 0.0f);
            dg.DrawImage(dg, "fadescreen", this._rect, true, this._position, false, color1);


            //top and bottom bits
            this._rect = new Rectangle(0, 292, 240, 20);
            this._position = new Vector2(0, 0-topoff);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);


            this._rect = new Rectangle(0, 323, 240, 20);
            this._position = new Vector2(0, 160-20+topoff);
            dg.DrawImage(dg, "title3", this._rect, true, this._position, false, Color.White);



            int menustartx = 60-(topoff*4);
            int menustarty = 112;
                //draw the actual options!
                Color color2;
                switch (this.plus)
                {
                    case 1:
                        color2 = Color.Yellow;
                        break;
                    case 2:
                        color2 = Color.Cyan;
                        break;
                    case 9:
                        color2 = Color.Red;
                        break;
                    default:
                        color2 = Color.White;
                        break;
                }
                var newGameSprite = ShanghaiEXE.languageTranslationService.GetLocalizedSprite("SceneTitle.NewGame");
                this._rect = newGameSprite.Item2;
                if (this.menu == SceneTitle.TITLEMENU.Load)
                    this._rect.X += this._rect.Width;
                this._position = new Vector2(menustartx - menuoffx, menustarty - menuoffy);
                //dg.DrawImage(dg, newGameSprite.Item1, this._rect, true, this._position, false, color2);

                //TODO: hook up localization

                dg.DrawText("New Game (Story)", this._position, true);
                //Console.WriteLine(this.fontposition.Y)
                this._position = new Vector2(menustartx - menuoffx, menustarty - menuoffy+16);
                //dg.DrawImage(dg, newGameSprite.Item1, this._rect, true, this._position, false, color2);
                dg.DrawText("New Game (Freeroam)", this._position, true);

                var continueSprite = ShanghaiEXE.languageTranslationService.GetLocalizedSprite("SceneTitle.Continue");
                this._rect = continueSprite.Item2;
                if (this.menu == SceneTitle.TITLEMENU.Load)
                    this._rect.X += this._rect.Width;
                this._position = new Vector2(menustartx - menuoffx, menustarty - menuoffy+32);
                dg.DrawText("Continue", this._position, true);
                this._position = new Vector2(menustartx - menuoffx, menustarty - menuoffy+ 48);
                string onoff;
                if (starforcemode == true)
                {
                onoff = "On";
                }
                else
                {
                onoff = "Off";
                }
                dg.DrawText("StrFrc Mode: " + onoff, this._position, true);
                this._position = new Vector2(menustartx - menuoffx, menustarty - menuoffy + 64);
                if (randomfolder == true)
                {
                    onoff = "On";
                }
                else
                {
                    onoff = "Off";
                }
                dg.DrawText("Random Fldr: " + onoff, this._position, true);


            //this._position.X = (float)(fontposition.X - (double)(this._rect.Width / 2) - 16.0);
            //arrow

            this._position.X = this._position.X - 8;
            this._position.Y = 128 - menuoffy-8;// + optionsel * 16;
            this._position.Y += optionsel * 16;

            this._rect = new Rectangle(240 + this.frame % 4 * 16, 192, 16, 16);
            dg.DrawImage(dg, "title", this._rect, false, this._position, false, Color.White);
            int num = 0;
            for (int index = 0; index < this.star.Length; ++index)
            {
                if (this.star[index])
                {
                    this._position.X = 144 + num;
                    this._position.Y = 0f;
                    this._rect = new Rectangle(632 + 16 * this.starAnime, index * 16, 16, 16);
                    dg.DrawImage(dg, "title2", this._rect, true, this._position, false, Color.White);
                    num += this.stars >= 5 ? 8 : 16;
                }
            }
            this._position = new Vector2(0.0f, 24f);
            this._rect = new Rectangle(440, 0, 64, 16);
            // dg.DrawImage(dg, "title", this._rect, true, this._position, false, Color.White);
            Color white2 = Color.White;
            string str = "v0.1.0";
            this._position = new Vector2(0.0f, 147 + topoff);

            dg.DrawMicroText(str, this._position, white2);
            if (ShanghaiEXE.Config.Seed != 0.0)
            {
                
                str = "Seed: " + ShanghaiEXE.Config.Seed.ToString();
                this._position = new Vector2(0, -2 - topoff);
                dg.DrawMicroText(str, this._position, white2);
            }
            else
            {
                
                this._position = new Vector2(0.0f, -2 - topoff);
                dg.DrawMicroText("Rando. Off", this._position, white2);
            }
        }

        private enum TITLESCENE
        {
            init,
            pushbutton,
            select,
            fade,
        }

        private enum TITLEMENU
        {
            Start,
            Load,
        }

        private enum STAR
        {
            シナリオクリア,
            裏ボス撃破,
            スタンダードコンプ,
            ナビチップコンプ,
            ダークチップコンプ,
            PAメモコンプ,
            SPウィルス全撃破,
            SP5ナビ全撃破,
            グリモワスタイル発現,
            ラスボスSP撃破,
        }
    }
}
