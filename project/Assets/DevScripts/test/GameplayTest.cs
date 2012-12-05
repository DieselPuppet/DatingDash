using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// ОТЛИЧНЫЙ ПЛАН

// генерация функциональных объектов
// перемещение персонажа
// перемещение по графам
// стек перемещений
// базовые реакции на персонажа (Action etc), взаимодействие с предметами по условиям
// 

public class GameplayTest : MonoBehaviour 
{	
	
}
/* 
public class IntroGame : guiScreenBase 
{
    UITexture _loadingSprite;
    
    private bool moviePlayed;
    private bool gameStarted;    
    AsyncOperation _loadOperation;
    private bool dowloadResoursesInvoked = false;
        
    private static IntroGame _instance;
    public static IntroGame instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (IntroGame)GameObject.FindObjectOfType(typeof(IntroGame));
            }
            return _instance;
        }
    }
    
    void Awake()
    {        
        //PlayerPrefs.DeleteAll();
        
        GL.Clear(true, true, Color.black);
        
        SystemManager.checkCurrentPlatform();
        
        base.init();
        
        _loadingSprite = transform.FindChild("Camera/Anchor/Panel/loadingSprite").GetComponent<UITexture>();
        _loadingSprite.gameObject.active = false;
        
        string textureName = "";
        
        if (Const.isChinaBuild)
        {
            if (SystemManager.platform == BangPlatform.iPad || SystemManager.platform == BangPlatform.iPad3)
                textureName = "Textures/iPad/loading_screen_ipad";
            else if (SystemManager.platform == BangPlatform.iPhone3GS)
                textureName = "Textures/iPhone/loading_screen_3gs";
            else if (SystemManager.platform == BangPlatform.iPhone5)
            {
                textureName = "Textures/iPhone5/loading_screen_iphone5";
                _loadingSprite.gameObject.transform.localScale = new Vector3(1084, _loadingSprite.gameObject.transform.localScale.y, _loadingSprite.gameObject.transform.localScale.z);
            }
            else 
                textureName = "Textures/iPhone/loading_screen";                    
        }
        else 
        {                
            if (SystemManager.platform == BangPlatform.iPad || SystemManager.platform == BangPlatform.iPad3)
                textureName = "Textures/iPad/catmoon_logo_ipad";
            else if (SystemManager.platform == BangPlatform.iPhone5)
            {
                textureName = "Textures/iPhone5/catmoon_logo_iphone5";
                _loadingSprite.gameObject.transform.localScale = new Vector3(1084, _loadingSprite.gameObject.transform.localScale.y, _loadingSprite.gameObject.transform.localScale.z);
            }
            else 
                textureName = "Textures/iPhone/catmoon_logo";
        }
        
        _loadingSprite.material.mainTexture = (Texture)Resources.Load(textureName);            
        
        Color c = _loadingSprite.material.color;
        c.a = 0;
        _loadingSprite.material.color = c;
    }
    
    void Start()
    {        
        GL.Clear(true, true, Color.black);    
        
        _loadOperation = Application.LoadLevelAdditiveAsync("stuff");        
        
        moviePlayed = false;
        gameStarted = false;            
        
        PlayIntroMovie();        
    }
    
    private void Update()
    {
        UpdateIntroMovie();

        if(moviePlayed)
        {
            if(!gameStarted && _loadOperation.isDone)
            {
                Invoke("startGame", 0.1f);
            }
        }
        
        if(dowloadResoursesInvoked && ContentManager.downloadingComplete)
        {
            dowloadResoursesInvoked = false;
            Debug.Log ("[IntroGame.loadGame]  closing  download popup");
            PopupManager.instance.closeCurrent();
            Debug.Log ("[IntroGame.loadGame]  invoke startGame() ");
            startGame();
        }
    	
    }
    
    void startGame()
    {        
        Debug.Log ("[IntroGame.startGame] start checking needed resourses");
        
        if(ContentManager.haveNeededContent())
        {
            if (Const.isChinaBuild)
            {
                Color c = _loadingSprite.material.color;    
                c.a = 255;
                _loadingSprite.material.color = c;            
                    
                _loadingSprite.gameObject.active = true;
                loadGame();
            }
            else 
            {    
                Debug.Log("IntroGame.start courutine");
                StartCoroutine(fullScreenFadeCor(null, 1f, true, showLogo));
            }
            
        }
        else
        {    
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log ("[IntroGame.startGame]  show popup about dowloading content net is unavlible");
                dowloadResoursesInvoked = true;
                PopupManager.instance.showPopup(PopupManager.instance._contentPopup);    
                PopupManager.instance.showPopup(PopupManager.instance._networkPopup);
            }
            else
            {
                Debug.Log ("[IntroGame.startGame]  show popup about dowloading content net is avlible");
                dowloadResoursesInvoked = true;
                PopupManager.instance.showPopup(PopupManager.instance._contentPopup);        
            }
        }
        gameStarted = true;                                         
    }
    
    void loadGame()
    {    
        
            Application.LoadLevel("MainMenu");    
            
    }
    
    
    public void startDownload()
    {
        Debug.Log ("[IntroGame.startDownload]  invoke ContentManager.downloadNededAssetBundles()");
        StartCoroutine(ContentManager.downloadNededAssetBundles());
    }
    
    
    
    void showLogo()
    {
        string textureName = "";
        
        if (SystemManager.platform == BangPlatform.iPad || SystemManager.platform == BangPlatform.iPad3)
            textureName = "Textures/iPad/loading_splash_ipad";
        else if (SystemManager.platform == BangPlatform.iPhone5)
            textureName = "Textures/iPhone5/loading_splash_iphone5";
        else 
            textureName = "Textures/iPhone/loading_splash";        
        
        _loadingSprite.material.mainTexture = (Texture)Resources.Load(textureName);
        
        StartCoroutine(fullScreenFadeCor(loadGame, 1f));
    }
    
#if UNITY_IPHONE

    private void PlayIntroMovie()
    {        
         // play movie
        Handheld.PlayFullScreenMovie("intro/Logo_iphone.mp4",
                                     Color.black,
                                     FullScreenMovieControlMode.CancelOnInput,
                                     FullScreenMovieScalingMode.AspectFit);
        
        // unity will be stopped during playback, so it receive control
        // when movie ended
        moviePlayed = true;
    }

    private void UpdateIntroMovie()
    {
        if (Input.anyKey || Input.GetMouseButton(0))
        {
            moviePlayed = true;        
        }
    }

#else    
    
    public MovieTexture m_introMovieTexture;
    private Rect m_screenRect;

    private void PlayIntroMovie()
    {
        m_screenRect = new Rect(0, 0, Screen.width, Screen.height);

        AudioSource movieAudio = gameObject.AddComponent<AudioSource>();
        movieAudio.clip = m_introMovieTexture.audioClip;
        movieAudio.loop = false;
        movieAudio.bypassEffects = true;

        gameObject.AddComponent<AudioListener>();

        m_introMovieTexture.Play();
        movieAudio.Play();
    }

    private void UpdateIntroMovie()
    {
        if(Input.GetMouseButton(0) || Input.anyKey)
            moviePlayed = true;

        if(!m_introMovieTexture.isPlaying)
            moviePlayed = true;
    }

    private void OnGUI()
    {
        if (!moviePlayed)
        GUI.DrawTexture(m_screenRect,
                        m_introMovieTexture,
                        ScaleMode.ScaleToFit);
    }    
    
#endif    
    
    public IEnumerator fullScreenFadeCor(Callback callback, float time, bool inOut = false, Callback finalCallback = null)
    {                       
        Color c = _loadingSprite.material.color;
        c.a = 0;
        _loadingSprite.material.color = c;
        
        _loadingSprite.gameObject.active = true;
            
        
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float factor = t / time;
            
            c.a = Mathf.Lerp(0, 1, factor);
            _loadingSprite.material.color = c;
        
            yield return null;
        }
        
        c.a = 1;
        _loadingSprite.material.color = c;
        
        if (callback != null)
               callback();        
        
        if (inOut)
        {
            yield return new WaitForSeconds(1);
        
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                float factor = t / time;
                c.a = Mathf.Lerp(1, 0, factor);    
                _loadingSprite.material.color = c;
            
                yield return null;
            }
            c.a = 0;
            
            _loadingSprite.material.color = c;    
            
            _loadingSprite.gameObject.active = false;
            
            if (finalCallback != null)
                finalCallback();
        }
    }    
}*/