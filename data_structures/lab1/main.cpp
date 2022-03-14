#include <iostream>

using namespace std;

// I took this from http://e-maxx.ru/algo/extended_euclid_algorithm
long gcdex(long a, long b, int & x, int & y) {
	if (a == 0) {
		x = 0;
		y = 1;
		return b;
	}
	int x1, y1;
	long d = gcdex(b % a, a, x1, y1);
	x = y1 - (b / a) * x1;
	y = x1;
	return d;
}

int main() {
	long a;
	long n;
	// example for big numbers: a=999994999, n=999999999 -> b=999799999
	cin >> a >> n;
	
	/*
	 my own solution, but 'example for big numbers' took 6 sec...
	 BUT!!! Works fine when 'n' < 'a'.
	 (a*b) % n = ((a % n) * (b % n)) % n
	 search for 'i' in [1, 2, 3, ..., n - 1]: ((a % n) * i) % n == 1
	 so 'i' will be required 'b'
	*/
//	long long remainderA = a % n; // long long to contain 1e9*1e9
//	for (int i = 1; i < n; i++) {
//		if ((remainderA * i) % n == 1) {
//			cout << i;
//
//			return 0;
//		}
//	}
//	cout << "0" << endl;
//	return 0;
	
	// solution with help of e-maxx.ru
	int x, y;
	int g = gcdex(a, n, x, y);
	
	if (g != 1) {
		cout << "0" << endl;
	} else {
		x = (x % n + n) % n;
		cout << x << endl;
	}
	
	cout << "Kuibarov Vyacheslav Nikolaevich 090304-RPIa-o21 " << endl;

	return 0;
}
