#include <iostream>
#include <random>
#include <stdlib.h> // rand srand
#include <fstream>
#include <iostream>
#include <chrono>
//#include "chrono_io"

using namespace std;

//int n = 4;
//int n = 1024;
int n = 2048 + 1024 + 1024;
string outputFileName = "output.txt";

unsigned int prevRandom = 5;
unsigned pseudoRandom() {
	prevRandom = prevRandom * 1537 % 1923455;

	return prevRandom % 100;
}


void fillMatrix(double* M) {
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			M[i * n + j] = pseudoRandom() % 100 + 0.3;
		}
	}
}

void outputMatrix(double* M) {
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			printf("%.1lf ", M[i * n + j]);
		}

		cout << endl;
	}

	cout << endl << endl;
}

void outputMatrixToFile(double* M) {
	ofstream file;

	file.open(outputFileName, ios::trunc);

	string tempStr = "";

	char buf[16];

	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			// left-aligned double with 1 digit after decimal point
			// (for buf[n] max length of string is = n-1, increase buf if you need more digits)
			snprintf(buf, sizeof(buf), "%-.1lf", M[i * n + j]);
			tempStr += buf;
			tempStr += " ";
		}

		file << tempStr << endl;
		tempStr = "";
	}

	cout << endl << endl;
}

// no optimisations
void multiply1(double* A, double* B, double* C) {
	int t;

	for (int i = 0; i < n; i++) {
		t = i * n;

		for (int j = 0; j < n; j++) {
			for (int k = 0; k < n; k++) {
				C[t + j] += A[t + k] * B[k * n + j];
			}
		}
	}
}

double* makeTransponatedMatrix(double* M) {
	double* T = new double[n * n];

	double* rowM;

	for (int i = 0; i < n; i++) {
		rowM = M + i * n;

		for (int j = 0; j < n; j++) {
			T[j * n + i] = rowM[j];
		}
	}

	return T;
}

void multiply2(double* A, double* B, double* C) {
	double* T = makeTransponatedMatrix(B);

	double* rowA;
	double* rowT;
	double* rowC;

	for (int i = 0; i < n; i++) {
		rowA = A + i * n;
		rowC = C + i * n;

		for (int j = 0; j < n; j++) {
			rowT = T + j * n;

			for (int k = 0; k < n; k++) {
				rowC[j] += rowA[k] * rowT[k];
			}
		}
	}
}

double multiplyVectors(double* vectorA, double* vectorB) {
	int n2 = (n / 4) * 4;
	double s1 = 0.0, s2 = 0.0, s3 = 0.0, s4 = 0.0;

	for (int i = 0; i < n2; i += 4) { // loop unroll x4
		s1 += vectorA[i] * vectorB[i];
		s2 += vectorA[i + 1] * vectorB[i + 1];
		s3 += vectorA[i + 2] * vectorB[i + 2];
		s4 += vectorA[i + 3] * vectorB[i + 3];
	}

	for (int i = n2; i < n; i++) {
		s1 += vectorA[i] * vectorB[i];
	}

	return (s4 + s3) + (s2 + s1);
}

void multiply3(double* A, double* B, double* C) {
	double* T = makeTransponatedMatrix(B);

	double* rowA;
	double* rowC;

	for (int i = 0; i < n; i++) {
		rowA = A + i * n;
		rowC = C + i * n;

		for (int j = 0; j < n; j++) {
			rowC[j] = multiplyVectors(rowA, T + j * n);
		}
	}
}

int main() {
	double* A = new double[n * n];
	double* B = new double[n * n];
	double* C = new double[n * n];

	for (int i = 0; i < n * n; i++) {
		C[i] = 0;
	}

	fillMatrix(A);
	fillMatrix(B);

	//outputMatrix(A); // not recommended if matrix is too big
	//outputMatrix(B);

	//outputMatrix(A);
	//outputMatrix(makeTransponatedMatrix(A));

	//multiply1(A, B, C);
	//multiply2(A, B, C);

	typedef chrono::high_resolution_clock Clock;
	auto t1 = Clock::now();

	multiply3(A, B, C);

	auto t2 = Clock::now();
	cout << chrono::duration_cast<chrono::milliseconds>(t2 - t1).count() << endl;

	//outputMatrix(C);
	//	outputMatrixToFile(C);

	return 0;
}



// ?????? ?????????: CTRL+F5 ??? ???? "???????" > "?????? ??? ???????"
// ??????? ?????????: F5 ??? ???? "???????" > "????????? ???????"
