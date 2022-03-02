#include <iostream>
#include <string>
#include <fstream>
#include <limits>

#include "ship.h"

using namespace std;

void Ship::changeWaterLevel(int unitsToAdd) {
	cout << "Current water level: " << this->waterLevel;
	
	this->waterLevel += unitsToAdd;
	
	cout << "New water level: " << this->waterLevel;
}

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
	
	cout << endl << "enter weight: ";
	getline(cin, t);
	this->totalWeight = stoi(t);
	
	cout << endl << "enter count of boats: ";
	getline(cin, t);
	this->countOfBoats = stoi(t);
	
	cout << endl << "enter boat weight: ";
	getline(cin, t);
	this->boatWeight = stoi(t);
	
	cout << endl << "enter waterLevel: ";
	getline(cin, t);
	this->waterLevel = stoi(t);
	
	cout << endl;
	
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
	
	file.open("struct.txt", ios_base::trunc); // очистить
	
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
	
	file.open("struct.txt");
	
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
	

//struct Ship {
//private:
//	string name;
//	int totalWeight;
//	int countOfBoats;
//	int boatWeight;
//	int waterLevel;
	
//	void changeWaterLevel(int unitsToAdd) {
//		cout << "Current water level: " << this->waterLevel;
//		
//		this->waterLevel += unitsToAdd;
//		
//		cout << "New water level: " << this->waterLevel;
//	}
//public:
//	Ship( // constructor
//		string name = "",
//		int shipWeight = 0,
//		int countOfBoats = 0,
//		int boatWeight = 0,
//		int waterLevel = 0
//	) {
//		this->name = name;
//		this->totalWeight = shipWeight + countOfBoats * boatWeight;
//		this->countOfBoats = countOfBoats;
//		this->boatWeight = boatWeight;
//		this->waterLevel = waterLevel;
//	}
	
	
//	void dropBoats(int countToDrop) {
//		if (countToDrop > this->countOfBoats) {
//			cout << "Not enough boats!" << endl;
//			
//			return;
//		}
//		
//		cout << "Ship weight before drop: " << this->totalWeight << endl;
//		
//		this->countOfBoats -= countToDrop;
//		this->totalWeight -= countToDrop * boatWeight;
//		
//		cout << "Boats dropped, new ship weight: " << this->totalWeight;
//	}
//	
//	void decreaseWaterLevel(int unitsToRemove) {
//		if (unitsToRemove > this->waterLevel) {
//			cout << "Too much water" << endl;
//			
//			return;
//		}
//		
//		this->changeWaterLevel(-unitsToRemove);
//	}
//	
//	void increaseWaterLevel(unsigned int unitsToAdd) {
//		this->changeWaterLevel(unitsToAdd);
//	}
//	
//	void fillFromConsole(){
//		string t;
//		
//		cout << "enter name: ";
//		getline(cin, this->name);
//		
//		cout << endl << "enter weight: ";
//		getline(cin, t);
//		this->totalWeight = stoi(t);
//		
//		cout << endl << "enter count of boats: ";
//		getline(cin, t);
//		this->countOfBoats = stoi(t);
//		
//		cout << endl << "enter boat weight: ";
//		getline(cin, t);
//		this->boatWeight = stoi(t);
//		
//		cout << endl << "enter waterLevel: ";
//		getline(cin, t);
//		this->waterLevel = stoi(t);
//		
//		cout << endl;
//		
//		this->totalWeight = this->totalWeight + this->boatWeight * this->countOfBoats;
//	}
//	
//	void outputToConsole() {
//		cout << "name: " << this->name << endl;
//		cout << "weight: " << this->totalWeight << endl;
//		cout << "count of boats: " << this->countOfBoats << endl;
//		cout << "boat weight: " << this->boatWeight << endl;
//		cout << "water level: " << this->waterLevel << endl;
//	}
//	
//	void outputToFile() {
//		ofstream file;
//		
//		file.open("struct.txt", ios_base::trunc); // очистить
//		
//		file << this->name << endl;
//		file << this->totalWeight << endl;
//		file << this->countOfBoats << endl;
//		file << this->boatWeight << endl;
//		file << this->waterLevel << endl;
//		
//		cout << "Saved" << endl;
//
//		file.close();
//	}
//	
//	void readFromFile() {
//		ifstream file;
//		
//		file.open("struct.txt");
//		
//		if (!file.is_open()) {
//			cout << "No file" << endl;
//		
//			return;
//		}
//		
//		string t;
//		
//		getline(file, this->name);
//		getline(file, t);
//		this->totalWeight = stoi(t);
//		getline(file, t);
//		this->countOfBoats = stoi(t);
//		getline(file, t);
//		this->boatWeight = stoi(t);
//		getline(file, t);
//		this->waterLevel = stoi(t);
//	}
//};
