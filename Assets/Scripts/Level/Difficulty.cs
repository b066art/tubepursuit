using System;
using System.Linq;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    private int[] obstacles = new int[6];

    private int obstacleMinLevel = 1;
    private int obstacleMaxLevel = 12;

    private int currentLevel = 36;

    private void Start() {
        FindMinLevel();
        GenerateArray();
    }

    private void FindMinLevel() {
        for (int i = 0; i < obstacles.Length; i++) { obstacles[i] = obstacleMinLevel + i; }
        if (currentLevel + obstacles.Length - 1 > (obstacles[0] + obstacles[obstacles.Length - 1]) * obstacles.Length / 2) {
            obstacleMinLevel++;
            FindMinLevel();
        }
    }

    private void GenerateArray() {
        if (obstacleMinLevel == 1) {
            for (int i = 0; i < obstacles.Length; i++) { obstacles[i] = 1; }

            for (int i = 1; i < obstacles.Length; i++) {
                for (int n = 0; n < obstacles.Length - i; n++) {
                    if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
                    obstacles[n] = i + 1;
                }
            }

            Array.Sort(obstacles);
        }

        else if (obstacleMinLevel + obstacles.Length > obstacleMaxLevel) {

            for (int i = 0; i < obstacles.Length; i++) {
                int n = obstacleMinLevel + i - 1;
                if (n > obstacleMaxLevel) { n = obstacleMaxLevel; }
                obstacles[i] = n;
            }

            while (obstacles.Sum() != obstacleMaxLevel * obstacles.Length) {
                for (int i = 0; i < obstacles.Length; i++) {
                    obstacles[i]++;
                    if (obstacles[i] > obstacleMaxLevel) { obstacles[i] = obstacleMaxLevel; }
                    if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
                }

                if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
            }
        }

        else {
            for (int i = 0; i < obstacles.Length; i++) { obstacles[i] = obstacleMinLevel + i - 1; }

            for (int i = 0; i < obstacles.Length; i++) {
                obstacles[0] = i + obstacleMinLevel;
                if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
            }

            Array.Sort(obstacles);

            if (obstacles[obstacles.Length - 1] >= obstacleMaxLevel) {
                obstacles[0]++;
                obstacles[obstacles.Length - 1]--;
            }
        }
    }

}
