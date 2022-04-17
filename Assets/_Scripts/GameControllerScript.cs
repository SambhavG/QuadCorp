using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using hf = HelperFunctions;



//TODO order of things:
//Add the rest of the level screens
//Add scoring and stars system
//Add a pause menu and level complete screen
//Add instructions
//Fix camera movement




//Jumbo screen possibilities:
//Beginning of the level, quadcorp logo and states which shape is going to be constructed, and countdown timer
//During level, comprehensive data from desiredShapeEquation and desiredShapesSize is shown
    //Possible forms: 
        //standard forms of the equation
        //the form which is solved for z
        //the shape of the figure (and traces)
        //the effect of a,b,c on the shape of the figure
        //axis of symmetry
        //Direction
        //special cases

//Ellipsoid possible formats
    //1. Direct equation x^2/a^2 + y^2/b^2 +z^2/c^2 = 1
    //2. Direct equation but denominators are solved
    //3. xy and xz traces
    //4. xy and yz traces
    //5. xz and yz traces

//Hyperboloid of one sheet possible formats
    //1. Direct equation x^2/a^2 + y^2/b^2 - z^2/c^2 = 1
    //2. Direct equation but denominators are solved
    //3. xy and xz traces
    //4. xy and yz traces
    //5. xz and yz traces

//Cone possible formats
    //1. Direct equation x^2/a^2 + y^2/b^2 - z^2/c^2 = 0
    //2. Direct equation but denominators are solved
    //3. xy and xz traces
    //4. xy and yz traces
    //5. xz and yz traces

//Hyperboloid of two sheets possible formats
    //1. Direct equation -x^2/a^2 - y^2/b^2 + z^2/c^2 = 1
    //2. Direct equation but denominators are solved
    //3. Traces for pair of planes which contain axis of symmetry

//Elliptic paraboloid possible formats
    //1. Direct equation x^2/a^2 + y^2/b^2 - z/c = 0
    //2. Direct equation but denominators are solved
    //3. Traces for pair of planes which contain axis of symmetry (SKIPPED FOR NOW)

//Hyperbolic paraboloid possible formats
    //1. Direct equation x^2/a^2 - y^2/b^2 - z/c = 0
    //2. Direct equation but denominators are solved
    //2. xy and xz traces (SKIPPED FOR NOW)
    //3. xy and yz traces (SKIPPED FOR NOW)
    //4. xz and yz traces (SKIPPED FOR NOW)

//The first and second shapes should be 1, then random for all other rounds


public class GameControllerScript : MonoBehaviour {

    //The game has several states

    //0: Creation
    //1: creation->station1 travel
    //2: create equation
    //3: station1->station2 travel
    //4: find abc
    //5: congratulations
    //6: station2->destruction
    //7: destruction and reset

    //Level and round variables
    public static int levelNumber = 2;
    public static int roundNumber = 1;
    private int roundState;

    //Target shape variables
    private int desiredShape;
    private int[] desiredShapeEquation;
    private int[] desiredShapeSize;

    //Prefabs to instantiate
    public Transform emptyAxes;

    public Transform blankEllipsoid;
    public Transform blankHyperboloidOfOneSheet;
    public Transform blankCone;
    public Transform blankHyperboloidOfTwoSheets;

    public Transform blankEllipticParaboloid;
    public Transform blankHyperbolicParaboloid;

    //Jumbotron and images
    public GameObject jumbotron;
    public TextMeshPro text_rightbigxp;
    public TextMeshPro text_rightbigyq;
    public TextMeshPro text_rightbigzr;
    public TextMeshPro text_rightsmallp;
    public TextMeshPro text_rightsmallq;
    public TextMeshPro text_rightsmallr;
    public TextMeshPro text_rightsmalls;

    public TextMeshPro text_bigneg1;
    public TextMeshPro text_bigneg2;
    public TextMeshPro text_bigneg3;
    public TextMeshPro text_bigpos2;
    public TextMeshPro text_bigpos3;
    public TextMeshPro text_smallneg1;
    public TextMeshPro text_smallneg2;
    public TextMeshPro text_smallneg3;
    public TextMeshPro text_smallneg4;
    public TextMeshPro text_smallpos2;
    public TextMeshPro text_smallpos4;


    
    public Texture2D ellipsoid0;
    public Texture2D ellipsoid1;
    public Texture2D ellipsoid2;
    public Texture2D ellipsoid3;
    public Texture2D ellipsoid4;
    public Texture2D ellipsoid5;
    public Texture2D hypOfOneSheet0;
    public Texture2D hypOfOneSheet1;
    public Texture2D hypOfOneSheet2;
    public Texture2D hypOfOneSheet3;
    public Texture2D hypOfOneSheet4;
    public Texture2D hypOfOneSheet5;
    public Texture2D cone0;
    public Texture2D cone1;
    public Texture2D cone2;
    public Texture2D cone3;
    public Texture2D cone4;
    public Texture2D cone5;
    public Texture2D hypOfTwoSheets0;
    public Texture2D hypOfTwoSheets1;
    public Texture2D hypOfTwoSheets2;
    public Texture2D hypOfTwoSheets3;
    public Texture2D hypOfTwoSheets4;
    public Texture2D hypOfTwoSheets5;
    public Texture2D ellipticParaboloid0;
    public Texture2D ellipticParaboloid11;
    public Texture2D ellipticParaboloid12;
    public Texture2D ellipticParaboloid13;
    public Texture2D ellipticParaboloid21;
    public Texture2D ellipticParaboloid22;
    public Texture2D ellipticParaboloid23;
    public Texture2D hyperbolicParaboloid0;
    public Texture2D hyperbolicParaboloid11;
    public Texture2D hyperbolicParaboloid12;
    public Texture2D hyperbolicParaboloid13;
    public Texture2D hyperbolicParaboloid21;
    public Texture2D hyperbolicParaboloid22;
    public Texture2D hyperbolicParaboloid23;

    //Doors
    public GameObject door1;
    public GameObject door2;

    //Ceiling (unhide when game starts)
    public GameObject ceiling;

    //roundState 2
    public new Camera camera;

    public float time;
    private float timeWhenLastShapeFinished;

    private int conveyorSpeed = 10;
    private Transform product;

    bool shapeCreated = false;
    
    GameObject[,] buttons = new GameObject[4,4];
    public GameObject x2;
    public GameObject y2;
    public GameObject z2;
    public GameObject nx2;
    public GameObject ny2;
    public GameObject nz2;
    public GameObject x;
    public GameObject y;
    public GameObject z;
    public GameObject nx;
    public GameObject ny;
    public GameObject nz;
    public GameObject one;
    public GameObject zero;

    private int[] equationSelections = {-1,-1,-1,-1};

    public Material buttonSelectedMaterial;
    public Material buttonDeselectedMaterial;

    public GameObject clippingPlanes;


    //roundState 4
    public GameObject station2;

    GameObject[] knobs = new GameObject[3];
    public GameObject knob1;
    public GameObject knob2;
    public GameObject knob3;

    
    Vector3 initialPosition = new Vector3(2.6f, 2.3f, 3.5f);
    Vector3 finalPosition = new Vector3(3.6244f, 2.3f, 3.0162f);

    private float leftBound = 0.0f;
    private float rightBound = 1.12f;
    private float knobIncrement;

    private GameObject selectedKnobInstance;

    private int selectedKnob = -1;

    private int[] knobSelections = {1,1,1};

    //roundState 5
    private float roundState5EndTime;


    // Start is called before the first frame update
    void Start() {
        //Set roundstate to preround
        roundState = 0; 
        //Reset timer and timing mechanism
        time = 0;
        timeWhenLastShapeFinished = 0;
        //Initialize selectedKnobInstance
        selectedKnobInstance = knob1;
        //Unhide ceiling
        ceiling.SetActive(true);
        //Place buttons and knobs in an array
        buttons[0,0] = x2;
        buttons[0,1] = nx2;
        buttons[0,2] = x;
        buttons[0,3] = nx;
        buttons[1,0] = y2;
        buttons[1,1] = ny2;
        buttons[1,2] = y;
        buttons[1,3] = ny;
        buttons[2,0] = z2;
        buttons[2,1] = nz2;
        buttons[2,2] = z;
        buttons[2,3] = nz;
        buttons[3,0] = zero;
        buttons[3,1] = one;
        buttons[3,2] = one;
        buttons[3,3] = one;

        knobs[0] = knob1;
        knobs[1] = knob2;
        knobs[2] = knob3;

        knobIncrement = (rightBound - leftBound) / 3;

        //Update jumbotron with starting screen
        jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid1;

        switch(levelNumber) {
            case 1:
                jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid0;
                break;
            case 2:
                jumbotron.GetComponent<Renderer>().material.mainTexture = hypOfOneSheet0;
                break;
            case 3:
                jumbotron.GetComponent<Renderer>().material.mainTexture = cone0;
                break;
            case 4:
                jumbotron.GetComponent<Renderer>().material.mainTexture = hypOfTwoSheets0;
                break;
            case 5:
                jumbotron.GetComponent<Renderer>().material.mainTexture = ellipticParaboloid0;
                break;
            case 6:
                jumbotron.GetComponent<Renderer>().material.mainTexture = hyperbolicParaboloid0;
                break;
            default:
                jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid0;
                break;
        }
        resetAllText();

        


    }

    void updateJumbotron(int inputShape, (int, int, int, int, int, int, int) inputShapeData, int inputRoundNumber) {

        int questionType;
        resetAllText();
        switch (inputShape) {
            case 1:
                //Ellipsoid
                //Determine which question type will be asked
                questionType = Random.Range(1,5);
                if (inputRoundNumber == 1 || inputRoundNumber == 2) {questionType = 1;}

                switch (questionType) {
                    case 1:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid1;
                        //Set a, b, c
                        text_rightbigxp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightbigyq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightbigzr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        break;
                    case 2:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid2;
                        //Set a, b, c
                        text_rightbigxp.GetComponent<TextMeshPro>().text = Mathf.Pow(hf.indexToScale(inputShapeData.Item5), 2).ToString();
                        text_rightbigyq.GetComponent<TextMeshPro>().text = Mathf.Pow(hf.indexToScale(inputShapeData.Item6), 2).ToString();
                        text_rightbigzr.GetComponent<TextMeshPro>().text = Mathf.Pow(hf.indexToScale(inputShapeData.Item7), 2).ToString();
                        break;
                    case 3:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid3;
                        //Set p, q, r, s
                        text_rightsmallp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmallq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmallr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmalls.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        break;
                    case 4:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid4;
                        //Set p, q, r, s
                        text_rightsmallp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmallq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmallr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmalls.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        break;
                    case 5:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = ellipsoid5;
                        //Set p, q, r, s
                        text_rightsmallp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmallq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        text_rightsmallr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmalls.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        break;
                }
            break;
            case 2:
                //Hyperboloid of one sheet
                //Determine which question type will be asked
                questionType = Random.Range(1,5);
                if (inputRoundNumber == 1 || inputRoundNumber == 2) {questionType = 1;}

                switch (questionType) {
                    case 1:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = hypOfOneSheet1;
                        //Set a, b, c
                        text_rightbigxp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightbigyq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightbigzr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        //set signs
                        if (inputShapeData.Item1 == 1 || inputShapeData.Item1 == 3) {
                            text_bigneg1.GetComponent<TextMeshPro>().text = "-";
                            //Set x position to -4.1
                            text_bigneg1.GetComponent<TextMeshPro>().transform.position = new Vector3(-4.1f, text_bigneg1.GetComponent<TextMeshPro>().transform.position.y, text_bigneg1.GetComponent<TextMeshPro>().transform.position.z);
                        }
                        if (inputShapeData.Item2 == 1 || inputShapeData.Item2 == 3) {
                            text_bigneg2.GetComponent<TextMeshPro>().text = "-";
                            //Set x position to -2.8
                            text_bigneg2.GetComponent<TextMeshPro>().transform.position = new Vector3(-2.8f, text_bigneg2.GetComponent<TextMeshPro>().transform.position.y, text_bigneg2.GetComponent<TextMeshPro>().transform.position.z);
                        } else {
                            text_bigpos2.GetComponent<TextMeshPro>().text = "+";
                            //Set x position to -2.78
                            text_bigpos2.GetComponent<TextMeshPro>().transform.position = new Vector3(-2.78f, text_bigpos2.GetComponent<TextMeshPro>().transform.position.y, text_bigpos2.GetComponent<TextMeshPro>().transform.position.z);
                        }
                        if (inputShapeData.Item3 == 1 || inputShapeData.Item3 == 3) {
                            text_bigneg3.GetComponent<TextMeshPro>().text = "-";
                            //Set x position to -1.45
                            text_bigneg3.GetComponent<TextMeshPro>().transform.position = new Vector3(-1.45f, text_bigneg3.GetComponent<TextMeshPro>().transform.position.y, text_bigneg3.GetComponent<TextMeshPro>().transform.position.z);
                        } else {
                            text_bigpos3.GetComponent<TextMeshPro>().text = "+";
                            //Set x position to -1.429
                            text_bigpos3.GetComponent<TextMeshPro>().transform.position = new Vector3(-1.429f, text_bigpos3.GetComponent<TextMeshPro>().transform.position.y, text_bigpos3.GetComponent<TextMeshPro>().transform.position.z);
                        }
                        break;
                    case 2:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = hypOfOneSheet2;
                        //Set a, b, c
                        text_rightbigxp.GetComponent<TextMeshPro>().text = Mathf.Pow(hf.indexToScale(inputShapeData.Item5), 2).ToString();
                        text_rightbigyq.GetComponent<TextMeshPro>().text = Mathf.Pow(hf.indexToScale(inputShapeData.Item6), 2).ToString();
                        text_rightbigzr.GetComponent<TextMeshPro>().text = Mathf.Pow(hf.indexToScale(inputShapeData.Item7), 2).ToString();
                        break;
                    case 3:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = hypOfOneSheet3;
                        //Set p, q, r, s
                        text_rightsmallp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmallq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmallr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmalls.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        break;
                    case 4:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = hypOfOneSheet4;
                        //Set p, q, r, s
                        text_rightsmallp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmallq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmallr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmalls.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        break;
                    case 5:
                        jumbotron.GetComponent<Renderer>().material.mainTexture = hypOfOneSheet5;
                        //Set p, q, r, s
                        text_rightsmallp.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item5).ToString();
                        text_rightsmallq.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        text_rightsmallr.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item6).ToString();
                        text_rightsmalls.GetComponent<TextMeshPro>().text = hf.indexToScale(inputShapeData.Item7).ToString();
                        break;
                }
            break;


        }
    }

    void resetAllText() {
        text_rightbigxp.GetComponent<TextMeshPro>().text = "";
        text_rightbigyq.GetComponent<TextMeshPro>().text = "";
        text_rightbigzr.GetComponent<TextMeshPro>().text = "";
        text_rightsmallp.GetComponent<TextMeshPro>().text = "";
        text_rightsmallq.GetComponent<TextMeshPro>().text = "";
        text_rightsmallr.GetComponent<TextMeshPro>().text = "";
        text_rightsmalls.GetComponent<TextMeshPro>().text = "";

        text_bigneg1.GetComponent<TextMeshPro>().text = "";
        text_bigneg2.GetComponent<TextMeshPro>().text = "";
        text_bigneg3.GetComponent<TextMeshPro>().text = "";
        text_bigpos2.GetComponent<TextMeshPro>().text = "";
        text_bigpos3.GetComponent<TextMeshPro>().text = "";
        text_smallneg1.GetComponent<TextMeshPro>().text = "";
        text_smallneg2.GetComponent<TextMeshPro>().text = "";
        text_smallneg3.GetComponent<TextMeshPro>().text = "";
        text_smallneg4.GetComponent<TextMeshPro>().text = "";
        text_smallpos2.GetComponent<TextMeshPro>().text = "";
        text_smallpos4.GetComponent<TextMeshPro>().text = "";
    }


    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;

        if (roundState == 0 && time-timeWhenLastShapeFinished > 3) {
            product = emptyAxes;
            product.transform.position = new Vector3(-5, 1, 14);

            //Generate a new desired shape
            //If level is 1...6, desired shape should be of type levelNumber
            //If level is 7, randomly generate a number for desired shape
            int desiredShape = levelNumber;
            if (levelNumber > 6) {
                desiredShape = Random.Range(1,7);
            }

            //Generate a new desired shape equation
            (int, int, int, int, int, int, int) desiredShapeData = hf.generateDesiredShape(desiredShape);

            //Place first 4 elems of desiredShapeData into desiredShapeEquation
            desiredShapeEquation = new int[] {desiredShapeData.Item1, desiredShapeData.Item2, desiredShapeData.Item3, desiredShapeData.Item4};
            desiredShapeSize = new int[] {desiredShapeData.Item5, desiredShapeData.Item6, desiredShapeData.Item7};


            //Update jumbotron with new desired shape
            updateJumbotron(desiredShape, desiredShapeData, roundNumber);



            //Update round number
            roundState = 1;
        } else if (roundState == 1) {

            //Conveyor belt moves to station 1
            if (product.position.z > 5) {
                product.position = new Vector3(-5, 1, product.position.z - 1 * Time.deltaTime * conveyorSpeed);
            } else if (product.position.x < -1) {
                product.position = new Vector3(product.position.x + 1 * Time.deltaTime * conveyorSpeed, 1, 5);
            } else {
                product.position = new Vector3(-1, 1, 5);
                roundState = 2;
            }

            //Open left door
            if (product.position.z > 8) {
                //Hide door
                door1.GetComponent<Renderer>().enabled = false;
            } else if (product.position.z < 8) {
                //Show door
                door1.GetComponent<Renderer>().enabled = true;
            }

        } else if (roundState == 2) {

            int buttonSelection = -1;

            if (Input.GetButtonDown("Fire1")) {

                Ray ray = new Ray(camera.transform.position, camera.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    for (int i = 0; i < buttons.GetLength(0); i++) {
                        for (int j = 0; j < buttons.GetLength(1); j++) {
                            if (hit.transform == buttons[i,j].transform) {
                                buttonSelection = i * buttons.GetLength(1) + j;
                                i = buttons.GetLength(0);
                                j = buttons.GetLength(1);
                            }
                        }
                    }
                }
            
                //Update colors of button and equation
                if (buttonSelection != -1) {
                    int buttonSelectionColumn = (int)Mathf.Floor((float)buttonSelection / 4);
                    int buttonSelectionRow = buttonSelection % 4;
                    //print(buttonSelection + " " + buttonSelectionRow + " " + buttonSelectionColumn);

                    //Set all buttons in column to deselected material
                    for (int i = 0; i < 4; i++) {
                        buttons[buttonSelectionColumn, i].GetComponent<Renderer>().material = buttonDeselectedMaterial;
                    }
                    //Set button to selected material
                    buttons[buttonSelectionColumn, buttonSelectionRow].GetComponent<Renderer>().material = buttonSelectedMaterial;

                    //Set equationSelections based on row and column
                    equationSelections[buttonSelectionColumn] = buttonSelectionRow;
                    
                    //Check if all cols have a selected button
                    if (hf.areAllElementsNotEqual(equationSelections, -1)) {
                        if (shapeCreated) {
                            Destroy(product.Find("Shape(Clone)").gameObject);
                        } else {
                            shapeCreated = true;
                        }
                        //Create new shape as child of product

                        (int, Vector3) equationType = hf.getEquationType(equationSelections);
                        int shapeNum = equationType.Item1;
                        Vector3 shapeRotation = equationType.Item2;
                        Quaternion shapeRotationQuaternion = Quaternion.Euler(shapeRotation);

                        switch(shapeNum) {
                            case 1:
                                Instantiate(blankEllipsoid, product.transform.position, Quaternion.identity, product);
                                product.transform.Find("Shape(Clone)").transform.GetChild(0).transform.rotation = shapeRotationQuaternion;
                                break;
                            case 2:
                                Instantiate(blankHyperboloidOfOneSheet, product.transform.position, Quaternion.identity, product);
                                product.transform.Find("Shape(Clone)").transform.GetChild(0).transform.rotation = shapeRotationQuaternion;
                                break;
                            case 3:
                                Instantiate(blankCone, product.transform.position, Quaternion.identity, product);
                                product.transform.Find("Shape(Clone)").transform.GetChild(0).transform.rotation = shapeRotationQuaternion;
                                break;
                            case 4:
                                Instantiate(blankHyperboloidOfTwoSheets, product.transform.position, Quaternion.identity, product);
                                product.transform.Find("Shape(Clone)").transform.GetChild(0).transform.rotation = shapeRotationQuaternion;
                                break;
                            case 5:
                                Instantiate(blankEllipticParaboloid, product.transform.position, Quaternion.identity, product);
                                product.transform.Find("Shape(Clone)").transform.GetChild(0).transform.rotation = shapeRotationQuaternion;
                                break;
                            case 6:
                                Instantiate(blankHyperbolicParaboloid, product.transform.position, Quaternion.identity, product);
                                product.transform.Find("Shape(Clone)").transform.GetChild(0).transform.rotation = shapeRotationQuaternion;
                                break;
                            default:
                                shapeCreated = false;
                                break;
                        }

                        if (hf.areIntArraysEqual(equationSelections, desiredShapeEquation)) {
                            //Set round state to 4
                            roundState = 3;
                        }
                    }
                }
            }
        } else if (roundState == 3) {
            if (product.position.x < 1) {
                product.position = new Vector3(product.position.x + 1 * Time.deltaTime * conveyorSpeed, 1, 5);
            } else {
                product.position = new Vector3(1, 1, 5);
                roundState = 4;
            }
        } else if (roundState == 4) {
            //User changes dials until correct combination is found

            //Check if user selected a dial
            if (Input.GetButtonDown("Fire1")) {
                Ray ray = new Ray(camera.transform.position, camera.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    for (int i = 0; i < knobs.Length; i++) {
                        if (hit.transform == knobs[i].transform) {
                            selectedKnob = i;
                            selectedKnobInstance = knobs[i];
                            i = knobs.Length;
                        }
                    }
                }
                
            }

            if (selectedKnob != -1) {
                bool done = false;

                //Move x position of selectedKnobInstance to mouse position
                float xPos = -1;
                Ray ray = new Ray(camera.transform.position, camera.transform.forward);
                RaycastHit hit;
                //float majorDistance = hf.xzDistance(initialPosition, finalPosition);

                if (Physics.Raycast(ray, out hit)) {
                    float zeroOneDistance = hf.distanceProportion(initialPosition, hit.point, finalPosition);
                    //Debug.Log(zeroOneDistance);
                    xPos = Mathf.Lerp(leftBound, rightBound, zeroOneDistance);
                }
                

                if (xPos < leftBound) {
                    xPos = leftBound;
                    selectedKnob = -1;
                } else if (xPos > rightBound) {
                    xPos = rightBound;
                    selectedKnob = -1;
                }
                

                selectedKnobInstance.transform.localPosition = new Vector3(xPos, selectedKnobInstance.transform.localPosition.y, selectedKnobInstance.transform.localPosition.z);

                //Check if user deselected a dial
                if (Input.GetButtonUp("Fire1")) {
                
                    //Snap recently deselected knob to its increment
                    selectedKnobInstance.transform.localPosition = new Vector3(Mathf.Round(selectedKnobInstance.transform.localPosition.x / knobIncrement) * knobIncrement, selectedKnobInstance.transform.localPosition.y, selectedKnobInstance.transform.localPosition.z);
                    knobSelections[selectedKnob] = (int)Mathf.Round((selectedKnobInstance.transform.localPosition.x - leftBound) / knobIncrement);
                    done = true;
                
                }

                //scale the product to match the knob
                float scale = (selectedKnobInstance.transform.localPosition.x-leftBound)/(2*knobIncrement) + 0.5f;
                Transform shape = product.Find("Shape(Clone)");

                if (selectedKnob == 0) {
                    shape.localScale = new Vector3(scale, shape.localScale.y, shape.localScale.z);
                } else if (selectedKnob == 1) {
                    shape.localScale = new Vector3(shape.localScale.x, shape.localScale.y , scale);
                } else if (selectedKnob == 2) {
                    shape.localScale = new Vector3(shape.localScale.x, scale, shape.localScale.z);
                }


                if (done) {
                    selectedKnob = -1;
                }
                
            } else {
                //Check if shape is correct
                if (hf.areIntArraysEqual(knobSelections, desiredShapeSize)) {
                    //Set round state to 5
                    roundState = 5;
                    roundState5EndTime = Time.time;
                }
            }

        } else if (roundState == 5) {
            //Set knobs to green material
            for (int i = 0; i < knobs.Length; i++) {
                knobs[i].GetComponent<Renderer>().material = buttonSelectedMaterial;
            }

            //Wait 2 seconds
            if (Time.time - roundState5EndTime > 2) {
                //Set round state to 6
                roundState = 6;
            }

        } else if (roundState == 6) {
            //Move finished product out
            if (product.position.x < 5) {
                product.position = new Vector3(product.position.x + 1 * Time.deltaTime * conveyorSpeed, 1, 5);
            } else if (product.position.z < 14) {
                product.position = new Vector3(5, 1, product.position.z + 1 * Time.deltaTime * conveyorSpeed);
            } else {
                product.position = new Vector3(5, 1, 14);
                roundState = 7;
            }

            if (product.position.z < 12) {
                //Hide door
                door2.GetComponent<Renderer>().enabled = false;
            } else if (product.position.z > 12) {
                //Show door
                door2.GetComponent<Renderer>().enabled = true;
            }

        } else if (roundState == 7) {
            //Destroy shape
            if (product.Find("Shape(Clone)") != null) {
                Destroy(product.Find("Shape(Clone)").gameObject);
                shapeCreated = false;
            }

            //Reset buttons and dials
            for (int i = 0; i < knobs.Length; i++) {
                knobs[i].transform.localPosition = new Vector3(leftBound+knobIncrement, knobs[i].transform.localPosition.y, knobs[i].transform.localPosition.z);
                knobSelections[i] = 1;
                knobs[i].GetComponent<Renderer>().material = buttonDeselectedMaterial;
            }
            for (int i = 0; i < equationSelections.Length; i++) {
                equationSelections[i] = -1;
            }
            for (int i = 0; i < buttons.GetLength(0); i++) {
                for (int j = 0; j < buttons.GetLength(1); j++) {
                    buttons[i,j].GetComponent<Renderer>().material = buttonDeselectedMaterial;
                }
            }

            //Update roundNumber
            roundNumber++;

            //Start next round
            roundState = 0;

        }
    }
}
