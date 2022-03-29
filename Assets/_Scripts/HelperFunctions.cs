using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions
{
    //Function to check if two int arrays are equal
    public static bool areIntArraysEqual(int[] arr1, int[] arr2) {
        //print elems of arr1
        //Debug.Log("arr1: " + arr1[0] + " " + arr1[1] + " " + arr1[2] + " " + arr1[3]);
        //print elems of arr2
        //Debug.Log("arr2: " + arr2[0] + " " + arr2[1] + " " + arr2[2] + " " + arr2[3]);


        if (arr1.Length != arr2.Length) {
            return false;
        }
        for (int i = 0; i < arr1.Length; i++) {
            if (arr1[i] != arr2[i]) {
                return false;
            }
        }
        return true;
    }

    //Fucntion to check if all elements in an array are not equal to some number
    public static bool areAllElementsNotEqual(int[] arr, int num) {
        for (int i = 0; i < arr.Length; i++) {
            if (arr[i] == num) {
                return false;
            }
        }
        return true;
    }

    //Function to determine what kind of equation was inputted
    //0: no type
    //1: ellipsoid
    //2: hyperboloid of one sheet
    //3: cone
    //4: hyperboloid of two sheets
    //5: elliptic paraboloid
    //6: hyperbolic paraboloid

    //Rotation: 1 means facing x, 2 means facing y, 3 means facing z, 4/5/6 are inverses, 0 means n/a, negatives mean rotated 90

    public static (int, Vector3) getEquationType(int[] eqsel) {

        if (areIntArraysEqual(eqsel, new int[] {0,0,0,1})) { //Ellipsoid
            return (1, new Vector3(0,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {1,0,0,1})) { //Hyperboloid of one sheet
            return (2, new Vector3(0,0,-90));
        } else if (areIntArraysEqual(eqsel, new int[] {0,1,0,1})) {
            return (2, new Vector3(90,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {0,0,1,1})) {
            return (2, new Vector3(0,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {1,0,0,0})) { //Cone
            return (3, new Vector3(0,0,-90));
        } else if (areIntArraysEqual(eqsel, new int[] {0,1,0,0})) {
            return (3, new Vector3(90,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {0,0,1,0})) {
            return (3, new Vector3(0,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {0,1,1,1})) { //Hyperboloid of two sheets
            return (4, new Vector3(0,0,-90));
        } else if (areIntArraysEqual(eqsel, new int[] {1,0,1,1})) {
            return (4, new Vector3(90,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {1,1,0,1})) {
            return (4, new Vector3(0,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {3,0,0,0})) { //Elliptic Paraboloid
            return (5, new Vector3(0,0,-90));
        } else if (areIntArraysEqual(eqsel, new int[] {3,1,1,0})) {
            return (5, new Vector3(0,0,90));
        } else if (areIntArraysEqual(eqsel, new int[] {0,3,0,0})) {
            return (5, new Vector3(90,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {1,3,1,0})) {
            return (5, new Vector3(-90,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {0,0,3,0})) {
            return (5, new Vector3(0,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {1,1,3,0})) {
            return (5, new Vector3(180,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {3,0,1,0})) { //Hyperbolic Paraboloid. Default orientation is x^2-y^2-z=0
            return (6, new Vector3(0,0,90));
        } else if (areIntArraysEqual(eqsel, new int[] {3,1,0,0})) {
            return (6, new Vector3(0,0,-90));
        } else if (areIntArraysEqual(eqsel, new int[] {0,3,1,0})) {
            return (6, new Vector3(90,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {1,3,0,0})) {
            return (6, new Vector3(-90,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {0,1,3,0})) {
            return (6, new Vector3(0,0,0));
        } else if (areIntArraysEqual(eqsel, new int[] {1,0,3,0})) {
            return (6, new Vector3(0,90,0));
        }

        return (0, new Vector3(0,0,0));
    }


    //Function to randomly generate a shape
    public static (int, int, int, int, int, int, int) generateDesiredShape(int shapeType) {
        int x, y, z, constant, a, b, c;
        int rand;
        a = Random.Range(0, 4);
        b = Random.Range(0, 4);
        c = Random.Range(0, 4);

        switch(shapeType) {
            case 1: //Ellipsoid
                x = 0;
                y = 0;
                z = 0;
                constant = 1;
                break;
            case 2:
                x = 0;
                y = 0;
                z = 0;
                rand = Random.Range(0, 3);
                if (rand == 0) {
                    x = 1;
                } else if (rand == 1) {
                    y = 1;
                } else {
                    z = 1;
                }
                constant = 1;
                break;
            case 3:
                x = 0;
                y = 0;
                z = 0;
                rand = Random.Range(0, 3);
                if (rand == 0) {
                    x = 1;
                } else if (rand == 1) {
                    y = 1;
                } else {
                    z = 1;
                }
                constant = 0;
                break;
            case 4:
                x = 1;
                y = 1;
                z = 1;
                rand = Random.Range(0, 3);
                if (rand == 0) {
                    x = 0;
                    a = Random.Range(0, 3);
                } else if (rand == 1) {
                    y = 0;
                    b = Random.Range(0, 3);
                } else {
                    z = 0;
                    c = Random.Range(0, 3);
                }
                constant = 1;
                break;
            case 5:
                x = 0;
                y = 0;
                z = 0;
                rand = Random.Range(0, 3);
                if (rand == 0) {
                    x = 3;
                } else if (rand == 1) {
                    y = 3;
                } else {
                    z = 3;
                }
                constant = 0;
                break;
            case 6:
                //Randomly assign 0, 1, 3 to x,y,z with no repeats
                x = Random.Range(0, 3);
                y = Random.Range(0, 3);
                z = Random.Range(0, 3);
                while (x == y || x == z || y == z) {
                    y = Random.Range(0, 3);
                    z = Random.Range(0, 3);
                }
                constant = 0;
                break;
            default:
                Debug.Log("Error");
                return (0, 0, 0, 0, 0, 0, 0);
        }
        return (x, y, z, constant, a, b, c);

    } 

}
