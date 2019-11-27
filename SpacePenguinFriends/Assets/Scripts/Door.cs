using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    private bool wasActivated;
    private const int maxPlates = 10;
    public int nPlates;
    private PressurePlate[] plates;
    public string[] plateNames;
    public bool requiresManualInteract;
    private bool finished;

    bool canBeDeactivated = false;

    // Start is called before the first frame update
    void Start()
    {
        wasActivated = false;
        finished = false;
        nPlates = 0;
        plates = new PressurePlate[maxPlates];

        for(int i = 0; i < maxPlates; ++i)
        {
            if (i >= plateNames.Length || plateNames[i] == null || plateNames[i].Length == 0)
            {
                continue;
            }
            LinkPlateByName(plateNames[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("wasActivated: " + wasActivated + ", finished:" + finished + ", canBeDeactivated: " + canBeDeactivated + "nPlates: " + nPlates);

        if (!wasActivated || canBeDeactivated)
            {
                for (int i = 0; i < nPlates; ++i)
                {
                
                    if (!plates[i].IsPressed())
                    {

                        if (finished)
                        {
                            OnDeactivation();
                        }
                        return;
                    }
                }
                wasActivated = true;
            }

            if (wasActivated && !finished)
            {
                OnActivation();
            }
    }

    public override void Interact()
    {
        if (!requiresManualInteract)
        {
            return;
        }

        if (wasActivated)
        {
            // Do something with the door only when activated
            transform.localScale += new Vector3(12.0f, 0.0f, 0.0f);
            finished = true;
        }
        else
        {
            // Maybe something different when it isn't?
        }
    }

    private void LinkPlateByName(string s)
    {
        if (nPlates >= maxPlates)
        {
            return;
        }

        GameObject go = GameObject.Find(s);
        PressurePlate pp = go.GetComponent<PressurePlate>();
        plates[nPlates++] = pp;
    }

    private void OnActivation()
    {
        Debug.Log("OnActivate");

        canBeDeactivated = true;

        if (requiresManualInteract)
        {
            return;
        }

        finished = true;
        // Do whatever the door should do when activated automatically
        //transform.localScale += new Vector3(0.0f, 10.0f, 0.0f);

        transform.position = transform.position + new Vector3(0, 1, 0);
    }

    private void OnDeactivation()
    {
        Debug.Log("OnDecativate");

        canBeDeactivated = false;

        wasActivated = false;
        finished = false;

        if (requiresManualInteract)
        {
            return;
        }
        // Do whatever the door should do when activated automatically
        //transform.localScale += new Vector3(0.0f, 10.0f, 0.0f);

        transform.position = transform.position + new Vector3(0, -1, 0);
    }
}
