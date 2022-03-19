#include <limits>
#include <iostream>

using namespace std;

void resetInputBuffer(){
	cin.clear();
	cin.ignore(numeric_limits<streamsize>::max(), '\n');
}
