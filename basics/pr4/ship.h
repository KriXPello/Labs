#include <string>

class Ship {
public:
	std::string name;
	int countOfBoats;
	int boatWeight;
	int waterLevel;
	
	Ship();
	
	Ship(
		std::string name,
		int totalWeight,
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
private:
	int totalWeight;
	
	void changeWaterLevel(int unitsToAdd);
};

