using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UpdateClimateData : MonoBehaviour {

    // ==================================================================
    // Objetos da cena:
    // ==================================================================

    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject sphere3D;
    [SerializeField] private GameObject sphereVR;

    [SerializeField] private Material insideoutMaterial;
    [SerializeField] private Material outsideMaterial;

    // ==================================================================
    // Variáveis de controle
    // ==================================================================

    private Quaternion initialRotation; 		        // Rotação inicial da camera

    private int projectionIndex = 2;		            // Indice inicial para projeção

    private int indexClimateData = 0;                   // Indice inicial para informações climáticas
    private int indexExhibitionDate = 3;                // Indice inicial para datas de exibição

    private int maxClimateDataIndex = 18;               // Limite máximo indexClimateData
    private int maximumExhibitionDateIndex = 5;         // Limite máximo para indexExhibitionDate

    private InterfaceController interfaceController;
    private CursorController cursorController;
    [HideInInspector] public bool animacaoEncerrada;    // Controle para liberar interação


    void Start ()
    {
        this.initialRotation = Camera.main.transform.rotation;

        this.interfaceController = FindObjectOfType<InterfaceController>();
        this.cursorController = FindObjectOfType<CursorController>();

        // Definindo informações exibidas inicialmente:
        this.ChangeInformation(0, 3);
    }


    private void OnApplicationQuit()
    {
        // Redefinir informações exibidas para as iniciais:
        this.ChangeInformation(0, 3);
    }


    void Update ()
    {
        // ==================================================================
        // Simula rotação da terra durante a projeção 3D:
        // ==================================================================

        sphere3D.transform.Rotate (Vector3.down / 25, Space.Self);
        sphere3D.transform.Rotate (Vector3.back / 100, Space.Self);


        if (animacaoEncerrada == true)
        {
            // ==================================================================
            //	Input para troca entre Projeções:
            // ==================================================================

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.UpdateProjection(this.projectionIndex);
            }

            // ==================================================================
            //	Input para troca entre Informações Climáticas:
            // ==================================================================

            if (Input.GetKeyDown(KeyCode.S))
            {
                indexClimateData--;
                this.UpdateInformation();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                indexClimateData++;
                this.UpdateInformation();
            }

            // ==================================================================
            //	Input para troca entre Datas de Exibição:
            // ==================================================================
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(indexClimateData > 0)
                {
                    indexExhibitionDate--;
                    this.UpdateInformation();
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (indexClimateData > 0)
                {
                    indexExhibitionDate++;
                    this.UpdateInformation();
                }
            }
        }
    }

    private void UpdateProjection (int viewIndex)
    {
        Camera.main.transform.rotation = this.initialRotation;

        switch (viewIndex)
        {
            //Projeção 2D (Equirectangular):
            case 1:

                Camera.main.GetComponent<CameraController>().enabled = false;
                //this.cursorController.LockCursor();

                plane.GetComponent<Renderer>().enabled = true;
                sphere3D.GetComponent<Renderer>().enabled = false;
                sphereVR.GetComponent<Renderer>().enabled = false;
                this.projectionIndex = 2;
                break;

            //Projeção 3D (Ortographic):
            case 2:

                Camera.main.GetComponent<CameraController>().enabled = false;
                //this.cursorController.LockCursor();

                plane.GetComponent<Renderer>().enabled = false;
                sphere3D.GetComponent<Renderer>().enabled = true;
                sphereVR.GetComponent<Renderer>().enabled = false;
                this.projectionIndex = 3;
                break;

            //Projeção VR (Stereographic):
            case 3:

                Camera.main.GetComponent<CameraController>().enabled = true;
                //this.cursorController.UnlockCursor();

                plane.GetComponent<Renderer>().enabled = false;
                sphere3D.GetComponent<Renderer>().enabled = false;
                sphereVR.GetComponent<Renderer>().enabled = true;
                this.projectionIndex = 1;
                break;
        }
    }


    private void UpdateInformation()
    {
        CheckClimateDataIndex();
        CheckExhibitionDateIndex();

        // Atualiza as informações exibidas (Imagem e Interface)
        ChangeInformation(this.indexClimateData, this.indexExhibitionDate);
    }


    private void ChangeInformation(int indexClimateData, int indexExhibitionDate)
    {
        // 'Resources.Load' acessa "/Assets/Resources/" logo, o string seguinte complementa o caminho do arquivo:
        string texturePath = "Images/ClimateData/" + indexExhibitionDate + "/" + indexClimateData;

        insideoutMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture)) as Texture;
        outsideMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture)) as Texture;

        // Altera informações na interface:
        this.interfaceController.UpdateInformationInterface(indexClimateData, indexExhibitionDate);
    }


    private void CheckClimateDataIndex()
    {
        // Checa os limintes (mínimo e máximo) de ClimateData
        if (this.indexClimateData < 0)
        {
            this.indexClimateData = 0;
        }
        else if (this.indexClimateData > this.maxClimateDataIndex)
        {
            this.indexClimateData = this.maxClimateDataIndex;
        }
    }


    private void CheckExhibitionDateIndex()
    {
        // Checa os limites (mínimo e máximo) de ExhibitionDate
        if (this.indexExhibitionDate < 1)
        {
            this.indexExhibitionDate = 1;
        }
        else if (this.indexExhibitionDate > this.maximumExhibitionDateIndex)
        {
            this.indexExhibitionDate = this.maximumExhibitionDateIndex;
        }
    }
}
