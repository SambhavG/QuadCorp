using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using hf = HelperFunctions;


//TODO:
//Add a jumbo screen for details of what the shape should be
//Add station 3
//Add gui/points system
//Add a pause menu
//Add a level complete screeen
//Fix the camera movement
//Level design of factory and station materials
//Info at beginning of the level stating how to make a shape

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
    //2. Solve for actual a,b,c: x^2/a^2 + y^2/b^2 + z^2/c^2 = 1/16, 1/4, 4, 16
    //3. xy and xz traces
    //4. xy and yz traces
    //5. xz and yz traces

//Hyperboloid of one sheet possible formats
    //1. Direct equation x^2/a^2 + y^2/b^2 - z^2/c^2 = 1
    //2. Solve for actual a,b,c: x^2/a^2 + y^2/b^2 - z^2/c^2 = 1/16, 1/4, 4, 16
    //3. xy and xz traces
    //4. xy and yz traces
    //5. xz and yz traces

//Cone possible formats
    //1. Direct equation x^2/a^2 + y^2/b^2 - z^2/c^2 = 0
    //2. xy and xz traces
    //3. xy and yz traces
    //4. xz and yz traces

//Hyperboloid of two sheets possible formats
    //1. Direct equation -x^2/a^2 - y^2/b^2 + z^2/c^2 = 1
    //2. Solve for actual a,b,c: x^2/a^2 - y^2/b^2 + z^2/c^2 = 1/16, 1/4, 4, 16
    //3. Traces for pair of planes which contain axis of symmetry

//Elliptic paraboloid possible formats
    //1. Direct equation x^2/a^2 + y^2/b^2 - z/c = 0
    //2. Traces for pair of planes which contain axis of symmetry

//Hyperbolic paraboloid possible formats
    //1. Direct equation x^2/a^2 - y^2/b^2 - z/c = 0
    //2. xy and xz traces
    //3. xy and yz traces
    //4. xz and yz traces

public class GameControllerScript : MonoBehaviour {

    //The game has several states

    //Preround (0): a shape has just been finished and a new one started, or game has just started
    //No shape (1): a fresh shape has been created and is going to the station
    //Create shape (2): User must press buttons to generate shape
    //Shape created (3): User got correct equation. Shape is moving to station 2.
    //Scale shape (4): User must slide dials to scale shape
    //Shape scaled (5): User got correct scaling. Shape is moving to station 3.
    //Color shape (6): User must press buttons to color shape
    //Shape colored (7): User got correct color. Shape is finished and moving out.

    //Level and round variables
    public static int levelNumber = 5;
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
    GameObject[] knobs = new GameObject[3];
    public GameObject knob1;
    public GameObject knob2;
    public GameObject knob3;

    private float leftBound = -0.3f;
    private float rightBound = 0.6f;
    private float knobIncrement;

    private GameObject selectedKnobInstance;

    private int selectedKnob = -1;

    private int[] knobSelections = {1,1,1};


    // Start is called before the first frame update
    void Start() {
        Debug.Log(levelNumber);
        //Set roundstate to preround
        roundState = 0;
        //Reset timer and timing mechanism
        time = 0;
        timeWhenLastShapeFinished = 0;
        //Initialize selectedKnobInstance
        selectedKnobInstance = knob1;
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
    }


    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;

        if (roundState == 0 && time-timeWhenLastShapeFinished > 3) {
            product = emptyAxes;
            product.transform.position = new Vector3(-5, 2, 9);

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

            Debug.Log(desiredShapeData);

            //Update round number
            roundState = 1;
        } else if (roundState == 1) {

            //Conveyor belt moves to station 1
            if (product.position.z > 5) {
                product.position = new Vector3(-5, 2, product.position.z - 1 * Time.deltaTime * conveyorSpeed);
            } else if (product.position.x < -2) {
                product.position = new Vector3(product.position.x + 1 * Time.deltaTime * conveyorSpeed, 2, 5);
            } else {
                product.position = new Vector3(-2, 2, 5);
                roundState = 2;
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
                            Debug.Log("Destroyed shape");
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
            if (product.position.x < 2) {
                product.position = new Vector3(product.position.x + 1 * Time.deltaTime * conveyorSpeed, 2, 5);
            } else {
                product.position = new Vector3(2, 2, 5);
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
                if (Physics.Raycast(ray, out hit)) {
                    xPos = hit.point.x;
                }

                if (xPos < leftBound) {
                    xPos = leftBound;
                } else if (xPos > rightBound) {
                    xPos = rightBound;
                }

                selectedKnobInstance.transform.position = new Vector3(xPos, selectedKnobInstance.transform.position.y, selectedKnobInstance.transform.position.z);

                //Check if user deselected a dial
                if (Input.GetButtonUp("Fire1")) {
                
                    //Snap recently deselected knob to its increment
                    selectedKnobInstance.transform.position = new Vector3(Mathf.Round(selectedKnobInstance.transform.position.x / knobIncrement) * knobIncrement, selectedKnobInstance.transform.position.y, selectedKnobInstance.transform.position.z);
                    knobSelections[selectedKnob] = (int)Mathf.Round((selectedKnobInstance.transform.position.x - leftBound) / knobIncrement);
                    Debug.Log((selectedKnobInstance.transform.position.x - leftBound) / knobIncrement);
                    done = true;
                
                }

                //scale the product to match the knob
                float scale = (selectedKnobInstance.transform.position.x-leftBound)/(2*knobIncrement) + 0.5f;
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
                }
            }

        } else if (roundState == 5) {
            //Move finished product out
            if (product.position.x < 5) {
                product.position = new Vector3(product.position.x + 1 * Time.deltaTime * conveyorSpeed, 2, 5);
            } else if (product.position.z < 9) {
                product.position = new Vector3(5, 2, product.position.z + 1 * Time.deltaTime * conveyorSpeed);
            } else {
                product.position = new Vector3(5, 2, 9);
                roundState = 6;
            }

        } else if (roundState == 6) {
            //Destroy shape
            if (product.Find("Shape(Clone)") != null) {
                Destroy(product.Find("Shape(Clone)").gameObject);
                shapeCreated = false;
            }

            //Reset buttons and dials
            for (int i = 0; i < knobs.Length; i++) {
                knobs[i].transform.position = new Vector3(leftBound+knobIncrement, knobs[i].transform.position.y, knobs[i].transform.position.z);
                knobSelections[i] = 1;
            }
            for (int i = 0; i < equationSelections.Length; i++) {
                equationSelections[i] = -1;
            }
            for (int i = 0; i < buttons.GetLength(0); i++) {
                for (int j = 0; j < buttons.GetLength(1); j++) {
                    buttons[i,j].GetComponent<Renderer>().material = buttonDeselectedMaterial;
                }
            }

            //Start next round
            roundState = 0;

        }
        
        //If m key pressed, print dial selections
        if (Input.GetKeyDown(KeyCode.M)) {
            Debug.Log("Knob selections: " + knobSelections[0] + " " + knobSelections[1] + " " + knobSelections[2]);
        }
    }
}
