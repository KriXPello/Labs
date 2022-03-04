#include <iostream>
#include <conio.h>

#include "ship.h"

using namespace std;

// variant 19

int main(int argc, char** argv) {
	Ship s1;
	
	while (true) {
		char action;
		
		cout << endl << endl;
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
				s1.fillFromConsole();
				break;
			}
			case '2': {
				s1.outputToConsole();
				break;
			}
			case '3': {
				s1.outputToFile();
				break;
			}
			case '4': {
				s1.readFromFile();
				break;
			}
			case '5': {
				int count;
				
				cout << "Count to drop: ";
				cin >> count;
				cout << endl;
				
				s1.dropBoats(count);
				
				break;
			}
			case '6': {
				int count;
				
				cout << "Count to change: ";
				cin >> count;
				cout << endl;
				
				s1.increaseWaterLevel(count);
				break;
			}
			case '7': {
				int count;
				
				cout << "Count to change: ";
				cin >> count;
				cout << endl;
				
				s1.decreaseWaterLevel(count);
				break;
			}
		}
	}
	
	system("pause");
	return 0;
}
