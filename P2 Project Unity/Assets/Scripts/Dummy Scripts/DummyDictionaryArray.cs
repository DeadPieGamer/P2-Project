using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Namespace so that other scripts don't know this exists
namespace Felix.Dummy
{
    public class DummyDictionaryArray : MonoBehaviour
    {
        [Tooltip("The dictionary containing WordCards arrays")] private Dictionary<DummyEnum, WordCards[]> myDic = new Dictionary<DummyEnum, WordCards[]>();
        [SerializeField, Tooltip("The basic path to the folder with the sets")] private string basicPath = "Dummy/";

        // Start is called before the first frame update
        void Start()
        {
            // For every enum value defined in DummyEnum, go through them
            foreach (DummyEnum enumValue in System.Enum.GetValues(typeof(DummyEnum)))
            {
                // Adds a new key and value to the dictionary
                // myDic.Add
                //
                // The key of this is the enumValue
                // enumValue
                //
                // The value of this is everything in the folder named the same as the enumValue
                // Resources.LoadAll(basicPath + enumValue.ToString(), typeof(WordCards)).Cast<WordCards>().ToArray())
                //
                // Key is what you tell the dictionary to get a file, like numbers in arrays
                // Value is the value you get when you give a key
                // Here the value is WordCards arrays
                // This means it creates a Dictionary entry for every enum value in DummyEnum, and assigns everything in the given folder to that Dictionary entry as a WordCards array
                //
                // enumValue is the seperate parts of the enum though, they're also named values, but are here differentiated by being called enum before
                myDic.Add(enumValue, Resources.LoadAll(basicPath + enumValue.ToString(), typeof(WordCards)).Cast<WordCards>().ToArray());
                
                // Says which folder it loaded, and how many files it found in there
                Debug.Log("Loaded " + myDic[enumValue].Length + " files in the " + enumValue.ToString() + " folder");
            }
        }
    }
}
