#include <iostream>

using namespace std;

// создаёт массив в два раза меньше чем arr
// заполняет его с конца arr
int* getReversedHalf(int* arr, int arrLength) {
    int* newArr = new int[arrLength / 2];

    for (int i = 0; i < arrLength / 2; i++) {
        newArr[i] = arr[arrLength - 1 - i];
    }

    return newArr;
}

/*
    4 4 0 0
    4 4 0 0
    0 0 0 0
    0 0 0 0

    Разворачивается и становится:

    2 2 0 0
    2 2 0 0
    2 2 0 0
    2 2 0 0
*/
void expandToBottom(int*** cube, int cubeSide, int count, int depth) {
    int subside = 1 << count; // 1, 2, 4, 8, 16, ...

    for (int i = 0; i < subside; i++) {
        for (int j = 0; j < subside; j++) {
            cube[subside + i][j] = getReversedHalf(cube[subside - 1 - i][j], depth);
        }
    }
}

/*
    2 2 0 0
    2 2 0 0
    2 2 0 0
    2 2 0 0

    Разворачивается и становится:

    1 1 1 1
    1 1 1 1
    1 1 1 1
    1 1 1 1
*/
void expandToRight(int*** cube, int cubeSide, int count, int depth) {
    int height = 2 << count; // 2, 4, 8, 16, ...
    int width = 1 << count; // 1, 2, 4, 8, ...

    for (int i = 0; i < height; i++) {
        for (int j = 0; j < width; j++) {
            cube[i][width + j] = getReversedHalf(cube[i][width - 1 - j], depth);
        }
    }
}

void printCubeIn2d(int*** cube, int cubeSide) {
    for (int i = 0; i < cubeSide; i++) {
        for (int j = 0; j < cubeSide; j++) {
            cout << cube[i][j][0] << " ";
        }

        cout << endl;
    }
}

int main()
{
    const int k = 3;
    const int cubeSide = 1 << k;
    const int total =  cubeSide * cubeSide;

    // создаём и заполняем трёхмерный массив. x - ширина, y - высота, z - глубина.
    // По x,y находятся "стопки" чисел, изначально они в (0, 0)
    // и снаружи вглубь идут как 1, 2, 3, 4, ..., total
    int*** cube = new int**[cubeSide];

    for (int i = 0; i < cubeSide; i++) {
        cube[i] = new int*[cubeSide];

        for (int j = 0; j < cubeSide; j++) {
            cube[i][j] = new int[total];
        }
    }

    // заполняем исходную стопку (0, 0) от 1 до total
    for (int k = 0; k < total; k++) {
        cube[0][0][k] = k + 1;
    }

    // depth - количество слоёв у "стопок"
    int depth = total;
    // сколько раз развернули вниз
    int bottomCount = 0;
    // сколько раз развернули вправо
    int rightCount = 0;

    while (depth != 1) {
        expandToBottom(cube, cubeSide, bottomCount, depth);
        depth /= 2;

        expandToRight(cube, cubeSide, rightCount, depth);
        depth /= 2;

        bottomCount++;
        rightCount++;
    }

    printCubeIn2d(cube, cubeSide);

    return 0;
}
