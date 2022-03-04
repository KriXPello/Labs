#include <string>

struct Ship {

	std::string name;
	int totalWeight;
	int countOfBoats;
	int boatWeight;
	int waterLevel;
	
	void changeWaterLevel(int unitsToAdd);

	Ship( // constructor
		std::string name,
		int shipWeight,
		int countOfBoats,
		int boatWeight,
		int waterLevel
	);

	void dropBoats(int countToDrop);
	
	void decreaseWaterLevel(int unitsToRemove);
	
	void increaseWaterLevel(unsigned int unitsToAdd);
	
	void fillFromConsole();
	
	void outputToConsole();
	
	void outputToFile();
	
	void readFromFile();
};

