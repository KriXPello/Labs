#include <iostream>
#include <conio.h>
#include <fstream>

#include "ship.h"
#include "resetInputBuffer.h"

using namespace std;

// variant 19

string FILE_NAME = "array_of_structures.txt";

int main(int argc, char** argv) {
	int shipsCount;
	
	cout << "Enter count of ships: ";
	cin >> shipsCount;
	resetInputBuffer();
	
	Ship *array = new Ship[shipsCount];
	
	for (int i = 0; i < shipsCount; i++) {
		array[i].init();
	}
	
	while (true) {
		char action;
		
		cout << endl << endl;
		cout << "1 - do action with object" << endl;
		cout << "2 - save array to file" << endl;
		cout << "3 - load array from file" << endl;
		cout << "4 - output array to console" << endl;
		cout << "5 - remove object from array" << endl;
		cout << "6 - add object to array" << endl;
		
		action = getch();
		
		switch (action) {
			case '1': {
				cout << endl << "Select index of object" << endl;
				
				for (int i = 0; i < shipsCount; i++) {
					cout << i << " - " << array[i].name << endl;
				}
				
				int selectedIndex;
				cin >> selectedIndex;
				resetInputBuffer();
				
				array[selectedIndex].doAction();
				
				break;
			}
			case '2': {
				ofstream file;
	
				file.open(FILE_NAME, ios_base::trunc); // очистить
				
				for (int i = 0; i < shipsCount; i++) {
					file << array[i].name << endl;
					file << array[i].totalWeight << endl;
					file << array[i].countOfBoats << endl;
					file << array[i].boatWeight << endl;
					file << array[i].waterLevel << endl;
				}
				
				cout << "Saved" << endl;
			
				file.close();
				
				break;
			}
			case '3': {
				ifstream file;
				
				file.open(FILE_NAME, ifstream::in);
				
				if (!file.is_open()) {
					cout << "No file" << endl;
				
					break;
				}
				
				for (int i = 0; i < shipsCount; i++) {	
					string t;
					
					getline(file, t);
					array[i].name = t;
					getline(file, t);
					array[i].totalWeight = stoi(t);
					getline(file, t);
					array[i].countOfBoats = stoi(t);
					getline(file, t);
					array[i].boatWeight = stoi(t);
					getline(file, t);
					array[i].waterLevel = stoi(t);
				}
				cout << "Loaded" << endl;
				
				file.close();
				
				break;
			}
			case '4': {
				for (int i = 0; i < shipsCount; i++) {
					array[i].outputToConsole();
				}
				break;
			}
			case '5': {
				if (shipsCount == 0) {
					cout << "No ships" << endl;
					break;
				}
				
				cout << endl << "Select index of object to delete:" << endl;
				
				for (int i = 0; i < shipsCount; i++) {
					cout << i << " - " << array[i].name << endl;
				}
				
				int selectedIndex;
				cin >> selectedIndex;
				resetInputBuffer();
				
				Ship *newArray = new Ship[shipsCount - 1];
				
				int k = 0;
				for (int i = 0; i < shipsCount; i++) {
					if (selectedIndex == i) {
						continue;
					}
					
					newArray[k++] = array[i];
				}
				
				shipsCount--;

				delete[] array;
				array = newArray;
				
				cout << "Deleted" << endl;

				break;
			}
			case '6': {
				Ship *newShip = new Ship();
				
				newShip->init();
				
				Ship *newArray = new Ship[shipsCount + 1];
				
				int i = 0;
				for (; i < shipsCount; i++) {
					newArray[i] = array[i];
				}

				newArray[i] = *newShip;
				
				shipsCount++;
				
				delete[] array;
				array = newArray;
				
				cout << "Added" << endl;
				
				break;
			}
		}
	}
	
	
	system("pause");
	return 0;
}
