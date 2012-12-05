// ManagerData is our custom class that holds our defined objects we want to store in XML format 
 public class ManagerData 
 { 
    // We have to define a default instance of the structure 
   public Data manage;
   public Adjustment adjustment;
    // Default constructor doesn't really do anything at the moment 
   public ManagerData() { } 
    
   // Anything we want to store in the XML file, we define it here 
   public struct Data 
   { 
      public int coinCount; 
      public int totalCoin; 
      public bool free; 
      public int  carID;
	  public int loopCount;
	  public int difficulty;
	  public int gameTime;
   }
	
	public struct Adjustment
	{
		public float fangxiang;
		public float youmen;
		public float shache;
	}
}
