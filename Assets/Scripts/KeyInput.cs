using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class KeyInput : MonoBehaviour
{

    private CommandReader command;

    public float down;

    public bool active = false;
    public bool caps = false;
    public bool shift = false;

    public float backStartTime;
    public bool backStart = false;

    public string text;

    public string[] priorInput = { "Ұ", "Ұ", "Ұ", "Ұ", "Ұ", "Ұ", "Ұ", "Ұ", "Ұ", "Ұ" };

    public int index = 0;
    //public string convertedText;

    public int lines = 0;
    public int charLimit;

    public float startTime;
    private float blinkThresh = 0.5f;
    private float textMove = 0.408f;
    private float pitchRandomness = 0.3f;

    public bool blinkOn = false;

    private Vector3 startPosText;

    public GameObject Tilde;
    public GameObject OneTop;
    public GameObject TwoTop;
    public GameObject ThreeTop;
    public GameObject FourTop;
    public GameObject FiveTop;
    public GameObject SixTop;
    public GameObject SevenTop;
    public GameObject EightTop;
    public GameObject NineTop;
    public GameObject ZeroTop;
    public GameObject MinusTop;
    public GameObject PlusTop;
    public GameObject Backspace;
    public GameObject Tab;
    public GameObject LeftBracket;
    public GameObject RightBracket;
    public GameObject LeftDash;
    public GameObject Q;
    public GameObject W;
    public GameObject E;
    public GameObject R;
    public GameObject T;
    public GameObject Y;
    public GameObject U;
    public GameObject A;
    public GameObject Caps;
    public GameObject Colon;
    public GameObject I;
    public GameObject Quote;
    public GameObject O;
    public GameObject P;
    public GameObject D;
    public GameObject F;
    public GameObject G;
    public GameObject S;
    public GameObject H;
    public GameObject J;
    public GameObject K;
    public GameObject L;
    public GameObject Comma;
    public GameObject C;
    public GameObject Z;
    public GameObject Period;
    public GameObject B;
    public GameObject X;
    public GameObject RightDash;
    public GameObject Return;
    public GameObject ShiftL;
    public GameObject V;
    public GameObject N;
    public GameObject M;
    public GameObject Apple;
    public GameObject Seven;
    public GameObject OptionL;
    public GameObject Enter;
    public GameObject ShiftR;
    public GameObject Clear;
    public GameObject MinusNPad;
    public GameObject Space;
    public GameObject OptionR;
    public GameObject Eight;
    public GameObject Four;
    public GameObject Lft;
    public GameObject Nine;
    public GameObject Rt;
    public GameObject Five;
    public GameObject One;
    public GameObject Six;
    public GameObject Up;
    public GameObject Two;
    public GameObject Down;
    public GameObject Three;
    public GameObject Zero;
    public GameObject PeriodNPad;
    public GameObject EnterNPad;

    public GameObject computerCamera;
    public GameObject walkingCamera;

    public GameObject screenText;

    public TextMesh textMesh;

    public AudioSource typingAudio;
    public AudioClip typingSound;



    // Use this for initialization
    void Start()
    {
        UnityEngine.VR.InputTracking.Recenter();
        typingSound = (AudioClip)Resources.Load("Sounds/Key");
        command = GetComponent<CommandReader>();
        startPosText = screenText.transform.position;
        textMesh = screenText.GetComponent<TextMesh>();
        typingAudio = Space.GetComponent<AudioSource>();
        textMesh.text = "|";
        startTime = Time.time;
    }

    void Update()
    {

        if (Input.anyKeyDown)
        {
            typingAudio.pitch = Random.Range(1.0f - pitchRandomness, 1.0f + pitchRandomness);
            typingAudio.PlayOneShot(typingSound);
        }


        // Recenter oculus function
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.VR.InputTracking.Recenter();
            
        }

        if (Input.GetKeyDown("`"))
        {
            float currentY = Tilde.transform.localPosition.y;
            float newY = currentY - down;
            Tilde.transform.localPosition = new Vector3(Tilde.transform.localPosition.x, newY, Tilde.transform.localPosition.z);

            if (shift == true)
            {
                text = text + "~";
            }
            else
            {
                text = text + "`";
            }
        }
        else if (Input.GetKeyUp("`"))
        {
            Tilde.transform.localPosition = new Vector3(Tilde.transform.localPosition.x, 0, Tilde.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            float currentY = OneTop.transform.localPosition.y;
            float newY = currentY - down;
            OneTop.transform.localPosition = new Vector3(OneTop.transform.localPosition.x, newY, OneTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "!";
            }
            else
            {
                text = text + "1";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            OneTop.transform.localPosition = new Vector3(OneTop.transform.localPosition.x, 0, OneTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float currentY = TwoTop.transform.localPosition.y;
            float newY = currentY - down;
            TwoTop.transform.localPosition = new Vector3(TwoTop.transform.localPosition.x, newY, TwoTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "@";
            }
            else
            {
                text = text + "2";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            TwoTop.transform.localPosition = new Vector3(TwoTop.transform.localPosition.x, 0, TwoTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            float currentY = ThreeTop.transform.localPosition.y;
            float newY = currentY - down;
            ThreeTop.transform.localPosition = new Vector3(ThreeTop.transform.localPosition.x, newY, ThreeTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "#";
            }
            else
            {
                text = text + "3";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ThreeTop.transform.localPosition = new Vector3(ThreeTop.transform.localPosition.x, 0, ThreeTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            float currentY = FourTop.transform.localPosition.y;
            float newY = currentY - down;
            FourTop.transform.localPosition = new Vector3(FourTop.transform.localPosition.x, newY, FourTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "$";
            }
            else
            {
                text = text + "4";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            FourTop.transform.localPosition = new Vector3(FourTop.transform.localPosition.x, 0, FourTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            float currentY = FiveTop.transform.localPosition.y;
            float newY = currentY - down;
            FiveTop.transform.localPosition = new Vector3(FiveTop.transform.localPosition.x, newY, FiveTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "%";
            }
            else
            {
                text = text + "5";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            FiveTop.transform.localPosition = new Vector3(FiveTop.transform.localPosition.x, 0, FiveTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            float currentY = SixTop.transform.localPosition.y;
            float newY = currentY - down;
            SixTop.transform.localPosition = new Vector3(SixTop.transform.localPosition.x, newY, SixTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "^";
            }
            else
            {
                text = text + "6";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SixTop.transform.localPosition = new Vector3(SixTop.transform.localPosition.x, 0, SixTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            float currentY = SevenTop.transform.localPosition.y;
            float newY = currentY - down;
            SevenTop.transform.localPosition = new Vector3(SevenTop.transform.localPosition.x, newY, SevenTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "&";
            }
            else
            {
                text = text + "7";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            SevenTop.transform.localPosition = new Vector3(SevenTop.transform.localPosition.x, 0, SevenTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            float currentY = EightTop.transform.localPosition.y;
            float newY = currentY - down;
            EightTop.transform.localPosition = new Vector3(EightTop.transform.localPosition.x, newY, EightTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "*";
            }
            else
            {
                text = text + "8";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            EightTop.transform.localPosition = new Vector3(EightTop.transform.localPosition.x, 0, EightTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            float currentY = NineTop.transform.localPosition.y;
            float newY = currentY - down;
            NineTop.transform.localPosition = new Vector3(NineTop.transform.localPosition.x, newY, NineTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "(";
            }
            else
            {
                text = text + "9";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            NineTop.transform.localPosition = new Vector3(NineTop.transform.localPosition.x, 0, NineTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            float currentY = ZeroTop.transform.localPosition.y;
            float newY = currentY - down;
            ZeroTop.transform.localPosition = new Vector3(ZeroTop.transform.localPosition.x, newY, ZeroTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + ")";
            }
            else
            {
                text = text + "0";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            ZeroTop.transform.localPosition = new Vector3(ZeroTop.transform.localPosition.x, 0, ZeroTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            float currentY = MinusTop.transform.localPosition.y;
            float newY = currentY - down;
            MinusTop.transform.localPosition = new Vector3(MinusTop.transform.localPosition.x, newY, MinusTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "_";
            }
            else
            {
                text = text + "-";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Minus))
        {
            MinusTop.transform.localPosition = new Vector3(MinusTop.transform.localPosition.x, 0, MinusTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Plus))
        {
            float currentY = PlusTop.transform.localPosition.y;
            float newY = currentY - down;
            PlusTop.transform.localPosition = new Vector3(PlusTop.transform.localPosition.x, newY, PlusTop.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "+";
            }
            else
            {
                text = text + "=";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Plus))
        {
            PlusTop.transform.localPosition = new Vector3(PlusTop.transform.localPosition.x, 0, PlusTop.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            float currentY = Backspace.transform.localPosition.y;
            float newY = currentY - down;
            Backspace.transform.localPosition = new Vector3(Backspace.transform.localPosition.x, newY, Backspace.transform.localPosition.z);
            text = text.Remove(text.Length - 1);
            backStartTime = Time.time;
        }
        if (Input.GetKey(KeyCode.Backspace))
        {
            if (Time.time - backStartTime > 0.08f)
            {
                if (backStart == false)
                {
                    if (Time.time - backStartTime > 0.5f)
                    {
                        backStartTime = Time.time;
                        text = text.Remove(text.Length - 1);
                        backStart = true;
                    }
                }

                else
                {
                    backStartTime = Time.time;
                    text = text.Remove(text.Length - 1);
                }
            }
        }

        else if (Input.GetKeyUp(KeyCode.Backspace))
        {
            Backspace.transform.localPosition = new Vector3(Backspace.transform.localPosition.x, 0, Backspace.transform.localPosition.z);
            backStart = false;
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            float currentY = Tab.transform.localPosition.y;
            float newY = currentY - down;
            Tab.transform.localPosition = new Vector3(Tab.transform.localPosition.x, newY, Tab.transform.localPosition.z);
            //text = text + "/t";
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            Tab.transform.localPosition = new Vector3(Tab.transform.localPosition.x, 0, Tab.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            float currentY = LeftBracket.transform.localPosition.y;
            float newY = currentY - down;
            LeftBracket.transform.localPosition = new Vector3(LeftBracket.transform.localPosition.x, newY, LeftBracket.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "{";
            }
            else
            {
                text = text + "[";
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftBracket))
        {
            LeftBracket.transform.localPosition = new Vector3(LeftBracket.transform.localPosition.x, 0, LeftBracket.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            float currentY = RightBracket.transform.localPosition.y;
            float newY = currentY - down;
            RightBracket.transform.localPosition = new Vector3(RightBracket.transform.localPosition.x, newY, RightBracket.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "{";
            }
            else
            {
                text = text + "[";
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightBracket))
        {
            RightBracket.transform.localPosition = new Vector3(RightBracket.transform.localPosition.x, 0, RightBracket.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            float currentY = LeftDash.transform.localPosition.y;
            float newY = currentY - down;
            LeftDash.transform.localPosition = new Vector3(LeftDash.transform.localPosition.x, newY, LeftDash.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "|";
            }
            else
            {
                text = text + "\\";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Backslash))
        {
            LeftDash.transform.localPosition = new Vector3(LeftDash.transform.localPosition.x, 0, LeftDash.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Colon))
        {
            float currentY = Colon.transform.localPosition.y;
            float newY = currentY - down;
            Colon.transform.localPosition = new Vector3(Colon.transform.localPosition.x, newY, Colon.transform.localPosition.z);
            if (shift == true)
            {
                text = text + ":";
            }
            else
            {
                text = text + ";";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Colon))
        {
            Colon.transform.localPosition = new Vector3(Colon.transform.localPosition.x, 0, Colon.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.W))
        {
            float currentY = W.transform.localPosition.y;
            float newY = currentY - down;
            W.transform.localPosition = new Vector3(W.transform.localPosition.x, newY, W.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "W";
            }
            else
            {
                text = text + "w";
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            W.transform.localPosition = new Vector3(W.transform.localPosition.x, 0, W.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            float currentY = Q.transform.localPosition.y;
            float newY = currentY - down;
            Q.transform.localPosition = new Vector3(Q.transform.localPosition.x, newY, Q.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "Q";
            }
            else
            {
                text = text + "q";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            Q.transform.localPosition = new Vector3(Q.transform.localPosition.x, 0, Q.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.DoubleQuote))
        {
            float currentY = Quote.transform.localPosition.y;
            float newY = currentY - down;
            Quote.transform.localPosition = new Vector3(Quote.transform.localPosition.x, newY, Quote.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "\"";
            }
            else
            {
                text = text + "\'";
            }
        }
        else if (Input.GetKeyUp(KeyCode.DoubleQuote))
        {
            Quote.transform.localPosition = new Vector3(Quote.transform.localPosition.x, 0, Quote.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.A))
        {
            float currentY = A.transform.localPosition.y;
            float newY = currentY - down;
            A.transform.localPosition = new Vector3(A.transform.localPosition.x, newY, A.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "A";
            }
            else
            {
                text = text + "a";
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            A.transform.localPosition = new Vector3(A.transform.localPosition.x, 0, A.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Y))
        {
            float currentY = Y.transform.localPosition.y;
            float newY = currentY - down;
            Y.transform.localPosition = new Vector3(Y.transform.localPosition.x, newY, Y.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "Y";
            }
            else
            {
                text = text + "y";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            Y.transform.localPosition = new Vector3(Y.transform.localPosition.x, 0, Y.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.R))
        {
            float currentY = R.transform.localPosition.y;
            float newY = currentY - down;
            R.transform.localPosition = new Vector3(R.transform.localPosition.x, newY, R.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "R";
            }
            else
            {
                text = text + "r";
            }
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            R.transform.localPosition = new Vector3(R.transform.localPosition.x, 0, R.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.T))
        {
            float currentY = T.transform.localPosition.y;
            float newY = currentY - down;
            T.transform.localPosition = new Vector3(T.transform.localPosition.x, newY, T.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "T";
            }
            else
            {
                text = text + "t";
            }
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            T.transform.localPosition = new Vector3(T.transform.localPosition.x, 0, T.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.U))
        {
            float currentY = U.transform.localPosition.y;
            float newY = currentY - down;
            U.transform.localPosition = new Vector3(U.transform.localPosition.x, newY, U.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "U";
            }
            else
            {
                text = text + "u";
            }
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            U.transform.localPosition = new Vector3(U.transform.localPosition.x, 0, U.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.E))
        {
            float currentY = E.transform.localPosition.y;
            float newY = currentY - down;
            E.transform.localPosition = new Vector3(E.transform.localPosition.x, newY, E.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "E";
            }
            else
            {
                text = text + "e";
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            E.transform.localPosition = new Vector3(E.transform.localPosition.x, 0, E.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            caps = true;
            float currentY = Caps.transform.localPosition.y;
            float newY = currentY - down;
            Caps.transform.localPosition = new Vector3(Caps.transform.localPosition.x, newY, Caps.transform.localPosition.z);
            //			Caps.transform.localPosition = new Vector3(Caps.transform.localPosition.x,newY,Caps.transform.localPosition.z);
        }
        else if (Input.GetKeyUp(KeyCode.CapsLock))
        {
            caps = false;
            Caps.transform.localPosition = new Vector3(Caps.transform.localPosition.x, 0, Caps.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            float currentY = Comma.transform.localPosition.y;
            float newY = currentY - down;
            Comma.transform.localPosition = new Vector3(Comma.transform.localPosition.x, newY, Comma.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "<";
            }
            else
            {
                text = text + ",";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Comma))
        {
            Comma.transform.localPosition = new Vector3(Comma.transform.localPosition.x, 0, Comma.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.D))
        {
            float currentY = D.transform.localPosition.y;
            float newY = currentY - down;
            D.transform.localPosition = new Vector3(D.transform.localPosition.x, newY, D.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "D";
            }
            else
            {
                text = text + "d";
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            D.transform.localPosition = new Vector3(D.transform.localPosition.x, 0, D.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.I))
        {
            float currentY = I.transform.localPosition.y;
            float newY = currentY - down;
            I.transform.localPosition = new Vector3(I.transform.localPosition.x, newY, I.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "I";
            }
            else
            {
                text = text + "i";
            }
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            I.transform.localPosition = new Vector3(I.transform.localPosition.x, 0, I.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Period))
        {
            float currentY = Period.transform.localPosition.y;
            float newY = currentY - down;
            Period.transform.localPosition = new Vector3(Period.transform.localPosition.x, newY, Period.transform.localPosition.z);
            if (shift == true)
            {
                text = text + ">";
            }
            else
            {
                text = text + ".";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Period))
        {
            Period.transform.localPosition = new Vector3(Period.transform.localPosition.x, 0, Period.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.F))
        {
            float currentY = F.transform.localPosition.y;
            float newY = currentY - down;
            F.transform.localPosition = new Vector3(F.transform.localPosition.x, newY, F.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "F";
            }
            else
            {
                text = text + "f";
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            F.transform.localPosition = new Vector3(F.transform.localPosition.x, 0, F.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.G))
        {
            float currentY = G.transform.localPosition.y;
            float newY = currentY - down;
            G.transform.localPosition = new Vector3(G.transform.localPosition.x, newY, G.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "G";
            }
            else
            {
                text = text + "g";
            }
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            G.transform.localPosition = new Vector3(G.transform.localPosition.x, 0, G.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.H))
        {
            float currentY = H.transform.localPosition.y;
            float newY = currentY - down;
            H.transform.localPosition = new Vector3(H.transform.localPosition.x, newY, H.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "H";
            }
            else
            {
                text = text + "h";
            }
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            H.transform.localPosition = new Vector3(H.transform.localPosition.x, 0, H.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.O))
        {
            float currentY = O.transform.localPosition.y;
            float newY = currentY - down;
            O.transform.localPosition = new Vector3(O.transform.localPosition.x, newY, O.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "O";
            }
            else
            {
                text = text + "o";
            }
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            O.transform.localPosition = new Vector3(O.transform.localPosition.x, 0, O.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.C))
        {
            float currentY = C.transform.localPosition.y;
            float newY = currentY - down;
            C.transform.localPosition = new Vector3(C.transform.localPosition.x, newY, C.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "C";
            }
            else
            {
                text = text + "c";
            }
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            C.transform.localPosition = new Vector3(C.transform.localPosition.x, 0, C.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.P))
        {
            float currentY = P.transform.localPosition.y;
            float newY = currentY - down;
            P.transform.localPosition = new Vector3(P.transform.localPosition.x, newY, P.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "P";
            }
            else
            {
                text = text + "p";
            }
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            P.transform.localPosition = new Vector3(P.transform.localPosition.x, 0, P.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.J))
        {
            float currentY = J.transform.localPosition.y;
            float newY = currentY - down;
            J.transform.localPosition = new Vector3(J.transform.localPosition.x, newY, J.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "J";
            }
            else
            {
                text = text + "j";
            }
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            J.transform.localPosition = new Vector3(J.transform.localPosition.x, 0, J.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.S))
        {
            float currentY = S.transform.localPosition.y;
            float newY = currentY - down;
            S.transform.localPosition = new Vector3(S.transform.localPosition.x, newY, S.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "S";
            }
            else
            {
                text = text + "s";
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            S.transform.localPosition = new Vector3(S.transform.localPosition.x, 0, S.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            float currentY = RightDash.transform.localPosition.y;
            float newY = currentY - down;
            RightDash.transform.localPosition = new Vector3(RightDash.transform.localPosition.x, newY, RightDash.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "?";
            }
            else
            {
                text = text + "/";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Slash))
        {
            RightDash.transform.localPosition = new Vector3(RightDash.transform.localPosition.x, 0, RightDash.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float currentY = Z.transform.localPosition.y;
            float newY = currentY - down;
            Z.transform.localPosition = new Vector3(Z.transform.localPosition.x, newY, Z.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "Z";
            }
            else
            {
                text = text + "z";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            Z.transform.localPosition = new Vector3(Z.transform.localPosition.x, 0, Z.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.K))
        {
            float currentY = K.transform.localPosition.y;
            float newY = currentY - down;
            K.transform.localPosition = new Vector3(K.transform.localPosition.x, newY, K.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "K";
            }
            else
            {
                text = text + "k";
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            K.transform.localPosition = new Vector3(K.transform.localPosition.x, 0, K.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.L))
        {
            float currentY = L.transform.localPosition.y;
            float newY = currentY - down;
            L.transform.localPosition = new Vector3(L.transform.localPosition.x, newY, L.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "L";
            }
            else
            {
                text = text + "l";
            }
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            L.transform.localPosition = new Vector3(L.transform.localPosition.x, 0, L.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.B))
        {
            float currentY = B.transform.localPosition.y;
            float newY = currentY - down;
            B.transform.localPosition = new Vector3(B.transform.localPosition.x, newY, B.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "B";
            }
            else
            {
                text = text + "b";
            }
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            B.transform.localPosition = new Vector3(B.transform.localPosition.x, 0, B.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            float currentY = Seven.transform.localPosition.y;
            float newY = currentY - down;
            Seven.transform.localPosition = new Vector3(Seven.transform.localPosition.x, newY, Seven.transform.localPosition.z);
            text = text + "7";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad7))
        {
            Seven.transform.localPosition = new Vector3(Seven.transform.localPosition.x, 0, Seven.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.X))
        {
            float currentY = X.transform.localPosition.y;
            float newY = currentY - down;
            X.transform.localPosition = new Vector3(X.transform.localPosition.x, newY, X.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "X";
            }
            else
            {
                text = text + "x";
            }
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            X.transform.localPosition = new Vector3(X.transform.localPosition.x, 0, X.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            float currentY = Apple.transform.localPosition.y;
            float newY = currentY - down;
            Apple.transform.localPosition = new Vector3(Apple.transform.localPosition.x, newY, Apple.transform.localPosition.z);

        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Apple.transform.localPosition = new Vector3(Apple.transform.localPosition.x, 0, Apple.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            float currentY = Enter.transform.localPosition.y;
            float newY = currentY - down;
            Enter.transform.localPosition = new Vector3(Enter.transform.localPosition.x, newY, Enter.transform.localPosition.z);

        }
        else if (Input.GetKeyUp(KeyCode.RightAlt))
        {
            Enter.transform.localPosition = new Vector3(Enter.transform.localPosition.x, 0, Enter.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.LeftWindows))
        {
            float currentY = OptionL.transform.localPosition.y;
            float newY = currentY - down;
            OptionL.transform.localPosition = new Vector3(OptionL.transform.localPosition.x, newY, OptionL.transform.localPosition.z);

        }
        else if (Input.GetKeyUp(KeyCode.LeftWindows))
        {
            OptionL.transform.localPosition = new Vector3(OptionL.transform.localPosition.x, 0, OptionL.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            float currentY = OptionR.transform.localPosition.y;
            float newY = currentY - down;
            OptionR.transform.localPosition = new Vector3(OptionR.transform.localPosition.x, newY, OptionR.transform.localPosition.z);

        }
        else if (Input.GetKeyUp(KeyCode.Menu))
        {
            OptionR.transform.localPosition = new Vector3(OptionR.transform.localPosition.x, 0, OptionR.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            float currentY = OptionR.transform.localPosition.y;
            float newY = currentY - down;
            OptionR.transform.localPosition = new Vector3(OptionR.transform.localPosition.x, newY, OptionR.transform.localPosition.z);

        }
        else if (Input.GetKeyUp(KeyCode.Menu))
        {
            OptionR.transform.localPosition = new Vector3(OptionR.transform.localPosition.x, 0, OptionR.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            float currentY = Four.transform.localPosition.y;
            float newY = currentY - down;
            Four.transform.localPosition = new Vector3(Four.transform.localPosition.x, newY, Four.transform.localPosition.z);
            text = text + "4";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            Four.transform.localPosition = new Vector3(Four.transform.localPosition.x, 0, Four.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Return))
        {
            float currentY = Return.transform.localPosition.y;
            float newY = currentY - down;
            Return.transform.localPosition = new Vector3(Return.transform.localPosition.x, newY, Return.transform.localPosition.z);

            execute();
            //text = text + "\n";
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            Return.transform.localPosition = new Vector3(Return.transform.localPosition.x, 0, Return.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.N))
        {
            float currentY = N.transform.localPosition.y;
            float newY = currentY - down;
            N.transform.localPosition = new Vector3(N.transform.localPosition.x, newY, N.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "N";
            }
            else
            {
                text = text + "n";
            }
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            N.transform.localPosition = new Vector3(N.transform.localPosition.x, 0, N.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.M))
        {
            float currentY = M.transform.localPosition.y;
            float newY = currentY - down;
            M.transform.localPosition = new Vector3(M.transform.localPosition.x, newY, M.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "M";
            }
            else
            {
                text = text + "m";
            }
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            M.transform.localPosition = new Vector3(M.transform.localPosition.x, 0, M.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.V))
        {
            float currentY = V.transform.localPosition.y;
            float newY = currentY - down;
            V.transform.localPosition = new Vector3(V.transform.localPosition.x, newY, V.transform.localPosition.z);
            if (shift == true)
            {
                text = text + "V";
            }
            else
            {
                text = text + "v";
            }
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            V.transform.localPosition = new Vector3(V.transform.localPosition.x, 0, V.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float currentY = ShiftL.transform.localPosition.y;
            float newY = currentY - down;
            ShiftL.transform.localPosition = new Vector3(ShiftL.transform.localPosition.x, newY, ShiftL.transform.localPosition.z);
            caps = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ShiftL.transform.localPosition = new Vector3(ShiftL.transform.localPosition.x, 0, ShiftL.transform.localPosition.z);
            caps = false;
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            float currentY = ShiftR.transform.localPosition.y;
            float newY = currentY - down;
            ShiftR.transform.localPosition = new Vector3(ShiftR.transform.localPosition.x, newY, ShiftR.transform.localPosition.z);
            caps = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ShiftR.transform.localPosition = new Vector3(ShiftR.transform.localPosition.x, 0, ShiftR.transform.localPosition.z);
            caps = false;
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Clear) || Input.GetKeyDown(KeyCode.Numlock))
        {
            float currentY = Clear.transform.localPosition.y;
            float newY = currentY - down;
            Clear.transform.localPosition = new Vector3(Clear.transform.localPosition.x, newY, Clear.transform.localPosition.z);
            text = "";

        }
        else if (Input.GetKeyUp(KeyCode.Clear) || Input.GetKeyUp(KeyCode.Numlock))
        {
            Clear.transform.localPosition = new Vector3(Clear.transform.localPosition.x, 0, Clear.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            float currentY = MinusNPad.transform.localPosition.y;
            float newY = currentY - down;
            MinusNPad.transform.localPosition = new Vector3(MinusNPad.transform.localPosition.x, newY, MinusNPad.transform.localPosition.z);
            text = text + "-";
        }
        else if (Input.GetKeyUp(KeyCode.KeypadDivide))
        {
            MinusNPad.transform.localPosition = new Vector3(MinusNPad.transform.localPosition.x, 0, MinusNPad.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            float currentY = Lft.transform.localPosition.y;
            float newY = currentY - down;
            Lft.transform.localPosition = new Vector3(Lft.transform.localPosition.x, newY, Lft.transform.localPosition.z);
            text = text + "+";
        }
        else if (Input.GetKeyUp(KeyCode.KeypadMultiply))
        {
            Lft.transform.localPosition = new Vector3(Lft.transform.localPosition.x, 0, Lft.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            float currentY = Rt.transform.localPosition.y;
            float newY = currentY - down;
            Rt.transform.localPosition = new Vector3(Rt.transform.localPosition.x, newY, Rt.transform.localPosition.z);
            text = text + "*";
        }
        else if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            Rt.transform.localPosition = new Vector3(Rt.transform.localPosition.x, 0, Rt.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            float currentY = Seven.transform.localPosition.y;
            float newY = currentY - down;
            Seven.transform.localPosition = new Vector3(Seven.transform.localPosition.x, newY, Seven.transform.localPosition.z);
            text = text + "7";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad7))
        {
            Seven.transform.localPosition = new Vector3(Seven.transform.localPosition.x, 0, Seven.transform.localPosition.z);
        }

        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            float currentY = Eight.transform.localPosition.y;
            float newY = currentY - down;
            Eight.transform.localPosition = new Vector3(Eight.transform.localPosition.x, newY, Eight.transform.localPosition.z);
            text = text + "8";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            Eight.transform.localPosition = new Vector3(Eight.transform.localPosition.x, 0, Eight.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            float currentY = Eight.transform.localPosition.y;
            float newY = currentY - down;
            Eight.transform.localPosition = new Vector3(Eight.transform.localPosition.x, newY, Eight.transform.localPosition.z);
            text = text + "8";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            Eight.transform.localPosition = new Vector3(Eight.transform.localPosition.x, 0, Eight.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            float currentY = Four.transform.localPosition.y;
            float newY = currentY - down;
            Four.transform.localPosition = new Vector3(Four.transform.localPosition.x, newY, Four.transform.localPosition.z);
            text = text + "4";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            Four.transform.localPosition = new Vector3(Four.transform.localPosition.x, 0, Four.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            float currentY = Five.transform.localPosition.y;
            float newY = currentY - down;
            Five.transform.localPosition = new Vector3(Five.transform.localPosition.x, newY, Five.transform.localPosition.z);
            text = text + "5";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            Five.transform.localPosition = new Vector3(Five.transform.localPosition.x, 0, Five.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            float currentY = Six.transform.localPosition.y;
            float newY = currentY - down;
            Six.transform.localPosition = new Vector3(Six.transform.localPosition.x, newY, Six.transform.localPosition.z);
            text = text + "6";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            Six.transform.localPosition = new Vector3(Six.transform.localPosition.x, 0, Six.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            float currentY = Zero.transform.localPosition.y;
            float newY = currentY - down;
            Zero.transform.localPosition = new Vector3(Zero.transform.localPosition.x, newY, Zero.transform.localPosition.z);
            text = text + "0";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad0))
        {
            Zero.transform.localPosition = new Vector3(Zero.transform.localPosition.x, 0, Zero.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            float currentY = One.transform.localPosition.y;
            float newY = currentY - down;
            One.transform.localPosition = new Vector3(One.transform.localPosition.x, newY, One.transform.localPosition.z);
            text = text + "1";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            One.transform.localPosition = new Vector3(One.transform.localPosition.x, 0, One.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            float currentY = Two.transform.localPosition.y;
            float newY = currentY - down;
            Two.transform.localPosition = new Vector3(Two.transform.localPosition.x, newY, Two.transform.localPosition.z);
            text = text + "2";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            Two.transform.localPosition = new Vector3(Two.transform.localPosition.x, 0, Two.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            float currentY = Three.transform.localPosition.y;
            float newY = currentY - down;
            Three.transform.localPosition = new Vector3(Three.transform.localPosition.x, newY, Three.transform.localPosition.z);
            text = text + "3";
        }
        else if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            Three.transform.localPosition = new Vector3(Three.transform.localPosition.x, 0, Three.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            float currentY = PeriodNPad.transform.localPosition.y;
            float newY = currentY - down;
            PeriodNPad.transform.localPosition = new Vector3(PeriodNPad.transform.localPosition.x, newY, PeriodNPad.transform.localPosition.z);
            text = text + ".";
        }
        else if (Input.GetKeyUp(KeyCode.KeypadPeriod))
        {
            PeriodNPad.transform.localPosition = new Vector3(PeriodNPad.transform.localPosition.x, 0, PeriodNPad.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        // if (Input.GetKeyDown(KeyCode.KeypadPlus))
        // {
        // 	float currentY = Up.transform.localPosition.y;
        // 	float newY = currentY - down;
        // 	Up.transform.localPosition = new Vector3(Up.transform.localPosition.x,newY,Up.transform.localPosition.z);
        // 	currentY = Dwn.transform.localPosition.y;
        // 	newY = currentY - down;
        // 	Dwn.transform.localPosition = new Vector3(Dwn.transform.localPosition.x,newY, Dwn.transform.localPosition.z);
        // 	text = text + "/";
        // }
        // else if (Input.GetKeyUp(KeyCode.KeypadPlus))
        // {
        // 	Up.transform.localPosition = new Vector3(Up.transform.localPosition.x,0,Up.transform.localPosition.z);
        // 	Dwn.transform.localPosition = new Vector3(Dwn.transform.localPosition.x,0, Dwn.transform.localPosition.z);
        // }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            float currentY = EnterNPad.transform.localPosition.y;
            float newY = currentY - down;
            EnterNPad.transform.localPosition = new Vector3(EnterNPad.transform.localPosition.x, newY, EnterNPad.transform.localPosition.z);

            execute();
            //text = text + "\n";
        }
        else if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            EnterNPad.transform.localPosition = new Vector3(EnterNPad.transform.localPosition.x, 0, EnterNPad.transform.localPosition.z);
        }
        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float currentY = Space.transform.localPosition.y;
            float newY = currentY - down;
            Space.transform.localPosition = new Vector3(Space.transform.localPosition.x, newY, Space.transform.localPosition.z);
            text = text + " ";
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Space.transform.localPosition = new Vector3(Space.transform.localPosition.x, 0, Space.transform.localPosition.z);
        }

        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // float currentY = Up.transform.localPosition.y;
            // float newY = currentY - down;
            // Up.transform.localPosition = new Vector3(Up.transform.localPosition.x,newY,Up.transform.localPosition.z);
            //text = text + " ";
            prevInput();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            // Up.transform.localPosition = new Vector3(Up.transform.localPosition.x,0,Up.transform.localPosition.z);
        }

        //-------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // float currentY = Down.transform.localPosition.y;
            // float newY = currentY - down;
            // Down.transform.localPosition = new Vector3(Down.transform.localPosition.x,newY,Down.transform.localPosition.z);
            //text = text + " ";
            nextInput();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Down.transform.localPosition = new Vector3(Down.transform.localPosition.x,0,Down.transform.localPosition.z);
        }


        //-------------------------------------------------------------------

        // Line space effect
        //convertedText = text;

        //int lines = convertedText.Length / charLimit;

        //for (int i = 1; i <= lines; i++)
        //{
        //          convertedText = convertedText.Insert((i * charLimit) + ((i-1)),"\n");
        //}

        // Move text z position if text goes off screen
        int remainder = text.Length - charLimit;
        if (remainder > 0)
        {
            screenText.transform.position = new Vector3(startPosText.x, startPosText.y, startPosText.z + (textMove * remainder));
        }

        // Cursor blink effect
        if (Time.time - startTime > blinkThresh)
        {
            startTime = Time.time;
            blinkOn = !blinkOn;
        }
        if (blinkOn)
        {
            textMesh.text = text + "|";
        }
        else
        {
            textMesh.text = text;
        }
    }

    // Function which clears input line, puts it in old line, and sends command to the script which reads commands
    void execute()
    {

        command.input(text);
        screenText.transform.position = new Vector3(startPosText.x, startPosText.y, startPosText.z);

        if (text != "")
        {
            for (index = 9; index > 0; index--)
            {
                priorInput[index] = priorInput[index - 1];
            }
            priorInput[0] = text;
            index = -1;

            //print(priorInput[0] +  " " + priorInput[1] +  " " + priorInput[2] +  " " + priorInput[3] +  " " + priorInput[4] +  " " + priorInput[5] +  " " + priorInput[6] +  " " + priorInput[7] +  " " + priorInput[8] +  " " + priorInput[9] +  " ");
        }
        text = "";
    }

    void nextInput()
    {
        print("next");
        // If we're not at at the end

        if (index == 0)
        {
            index = -1;
            text = "";
        }

        if (index > -1)
        {
            index--;
            while ((priorInput[index] == "Ұ" || priorInput[index] == "") && index > 0)
            {
                index--;
            }
            if (priorInput[index] != "Ұ" && priorInput[index] != "Ұ")
            {
                text = priorInput[index];
                //textMesh.text = text;
            }
        }
    }

    void prevInput()
    {
        print("prior");
        if ((index + 1) < priorInput.Length && (priorInput[index + 1] != "Ұ" && priorInput[index + 1] != ""))
        {
            index++;
            text = priorInput[index];
            //textMesh.text = text;
        }
    }

}