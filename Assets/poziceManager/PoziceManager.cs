using UnityEngine;

public class PoziceManager 
{
    private static PoziceManager instance;

    public static PoziceManager Instance
    {
        get
        {
            
            if (instance == null)
            {
                instance = new PoziceManager(0); 
            }
            return instance;
        }
    }

    public Pozice aktPosition;

    public void Changed(int cisloPozice)
    {
        instance = new PoziceManager(cisloPozice);
    }

    public PoziceManager(int cisloPozice)
    {
        aktPosition = new boxPosition();
        switch (cisloPozice)
        {
            case 0: 
                aktPosition = new boxPosition(); break;
            case 1: 
                aktPosition = new circlePosition(); break;
            case 2:
                aktPosition = new trianglePosition(); break;
        }

    }
}
