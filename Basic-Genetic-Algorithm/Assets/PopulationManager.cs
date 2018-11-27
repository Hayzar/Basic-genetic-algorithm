using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    public GameObject populationUnit;
    public int populationSize = 10;
    public List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;

    [SerializeField] private int trialTime = 20;
    private int generation = 1;

    public Text generationText;
    public Text elapsedText;

    private float secondTime = 1;

    // Use this for initialization
    private void Start()
    {
        if (generationText!=null)
            generationText.text=generation.ToString();

        if (elapsedText!=null)
            elapsedText.text=((int)elapsed).ToString();

        for (int i = 0; i<populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-16, 16), 1f, Random.Range(-12, 12));
            GameObject go = Instantiate(populationUnit, pos, Quaternion.identity);
            DNA dna = go.GetComponent<DNA>();
            dna.red=Random.Range(0f, 1f);
            dna.blue=Random.Range(0f, 1f);
            dna.green=Random.Range(0f, 1f);

            float r = Random.Range(2, 8);
            go.transform.localScale=new Vector3(r, r, r);

            population.Add(go);
        }
    }

    private void Update()
    {
        elapsed+=Time.deltaTime;
        if (elapsed>trialTime)
        {
            BreedNewPopulation();
            elapsed=0;
        }

        secondTime-=Time.deltaTime;
        if (secondTime<=0)
        {
            elapsedText.text=((int)elapsed).ToString();
            secondTime=1f;
        }
    }

    private void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToEliminate).ToList();

        population.Clear();

        for (int i = (int)(sortedList.Count/2f)-1; i<sortedList.Count-1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i+1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }

        for (int i = 0; i<sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }

        generation++;
        generationText.text=generation.ToString();
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-16, 16), 1f, Random.Range(-12, 12));
        GameObject child = Instantiate(populationUnit, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        Vector3 childScale;
        Vector3 parent1Scale = parent1.transform.localScale;
        Vector3 parent2Scale = parent2.transform.localScale;

        DNA childDNA = child.GetComponent<DNA>();

        if (Random.Range(0, 500)>5)
        {
            childDNA.red=Random.Range(0, 10)<5 ? dna1.red : dna2.red;
            childDNA.green=Random.Range(0, 10)<5 ? dna1.green : dna2.green;
            childDNA.blue=Random.Range(0, 10)<5 ? dna1.blue : dna2.blue;
            childScale.x=Random.Range(0, 10)<5 ? parent1Scale.x : parent2Scale.x;
            childScale.y=Random.Range(0, 10)<5 ? parent1Scale.y : parent2Scale.y;
            childScale.z=Random.Range(0, 10)<5 ? parent1Scale.z : parent2Scale.z;
        }
        else
        {
            childDNA.red=Random.Range(0f, 1f);
            childDNA.blue=Random.Range(0f, 1f);
            childDNA.green=Random.Range(0f, 1f);
            childScale=new Vector3(50, 50, 50);
        }

        child.transform.localScale=childScale;

        return child;
    }
}