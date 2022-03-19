#include <iostream>
#include <string>
#include <fstream>
#include <limits>
#include <conio.h>

#include "ship.h"
#include "resetInputBuffer.h"

using namespace std;

void Ship::changeWaterLevel(int unitsToAdd) {
	cout << "Current water level: " << this->waterLevel;
	
	this->waterLevel += unitsToAdd;
	
	cout << "New water level: " << this->waterLevel;
}

Ship::Ship(): Ship("", 0, 0, 0, 0) {};

Ship::Ship( // constructor
	string name,
	int shipWeight,
	int countOfBoats,
	int boatWeight,
	int waterLevel
) {
	this->name = name;
	this->totalWeight = shipWeight + countOfBoats * boatWeight;
	this->countOfBoats = countOfBoats;
	this->boatWeight = boatWeight;
	this->waterLevel = waterLevel;
}

void Ship::dropBoats(int countToDrop) {
	if (countToDrop > this->countOfBoats) {
		cout << "Not enough boats!" << endl;
		
		return;
	}
	
	cout << "Ship weight before drop: " << this->totalWeight << endl;
	
	this->countOfBoats -= countToDrop;
	this->totalWeight -= countToDrop * boatWeight;
	
	cout << "Boats dropped, new ship weight: " << this->totalWeight;
}


void Ship::decreaseWaterLevel(int unitsToRemove) {
	if (unitsToRemove > this->waterLevel) {
		cout << "Too much water" << endl;
		
		return;
	}
	
	this->changeWaterLevel(-unitsToRemove);
}

void Ship::increaseWaterLevel(unsigned int unitsToAdd) {
	this->changeWaterLevel(unitsToAdd);
}

void Ship::fillFromConsole(){
	string t;
	
	cout << "enter name: ";
	getline(cin, this->name);
	
	cout << "enter weight: ";
	getline(cin, t);
	this->totalWeight = stoi(t);
	
	cout << "enter count of boats: ";
	getline(cin, t);
	this->countOfBoats = stoi(t);
	
	cout << "enter boat weight: ";
	getline(cin, t);
	this->boatWeight = stoi(t);
	
	cout << "enter waterLevel: ";
	getline(cin, t);
	this->waterLevel = stoi(t);
	
	this->totalWeight = this->totalWeight + this->boatWeight * this->countOfBoats;
}

void Ship::outputToConsole() {
	cout << "name: " << this->name << endl;
	cout << "weight: " << this->totalWeight << endl;
	cout << "count of boats: " << this->countOfBoats << endl;
	cout << "boat weight: " << this->boatWeight << endl;
	cout << "water level: " << this->waterLevel << endl;
}

void Ship::outputToFile() {
	ofstream file;
	
	string fileName;
	
	cout << "OUTPUT to file." << endl;
	cout << "Enter file name (with extension): ";
	cin >> fileName;
	resetInputBuffer();
	
	file.open(fileName, ios_base::trunc); // очистить
	
	file << this->name << endl;
	file << this->totalWeight << endl;
	file << this->countOfBoats << endl;
	file << this->boatWeight << endl;
	file << this->waterLevel << endl;
	
	cout << "Saved" << endl;

	file.close();
}

void Ship::readFromFile() {
	ifstream file;
	
	string fileName;
	
	cout << "Enter file name (with extension): ";
	cin >> fileName;
	resetInputBuffer();
	
	file.open(fileName);
	
	if (!file.is_open()) {
		cout << "No file" << endl;
	
		return;
	}
	
	string t;
	
	getline(file, this->name);
	getline(file, t);
	this->totalWeight = stoi(t);
	getline(file, t);
	this->countOfBoats = stoi(t);
	getline(file, t);
	this->boatWeight = stoi(t);
	getline(file, t);
	this->waterLevel = stoi(t);
}
	
void Ship::init() {
	char action;
		
	
	cout << endl;
	cout << "Choose way to init object:" << endl;
	cout << "1 - fill from console" << endl;
	cout << "2 - read from file" << endl;
	cout << "other - skip" << endl;
	
	action = getch();
	
	switch (action) {
		case '1': {
			this->fillFromConsole();
			break;
		}
		case '2': {
			this->readFromFile();
			break;
		}
	}
}

void Ship::doAction() {
	char action;
		
	cout << endl;
	cout << "Select action with object:" << endl;
	cout << "1 - fill from console" << endl;
	cout << "2 - output to console" << endl;
	cout << "3 - write to file" << endl;
	cout << "4 - read from file" << endl;
	cout << "5 - drop boats" << endl;
	cout << "6 - increase water level" << endl;
	cout << "7 - decrease water level" << endl;
	
	action = getch();
	
	switch (action) {
		case '1': {
			this->fillFromConsole();
			break;
		}
		case '2': {
			this->outputToConsole();
			break;
		}
		case '3': {
			this->outputToFile();
			break;
		}
		case '4': {
			this->readFromFile();
			break;
		}
		case '5': {
			int count;
			
			cout << "Count to drop: ";
			cin >> count;
			cout << endl;
			
			this->dropBoats(count);
			
			break;
		}
		case '6': {
			int count;
			
			cout << "Count to change: ";
			cin >> count;
			cout << endl;
			
			this->increaseWaterLevel(count);
			break;
		}
		case '7': {
			int count;
			
			cout << "Count to change: ";
			cin >> count;
			cout << endl;
			
			this->decreaseWaterLevel(count);
			break;
		}
	}
	
	system("pause");
}
