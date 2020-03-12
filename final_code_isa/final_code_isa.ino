#include<SoftwareSerial.h>
int pin1 = 9, pin2 = 10, pin3 = 11, pin4 =12 ;
int ir_pin = 2;
SoftwareSerial HC05(2, 3);

void setup() {
  // put your setup code here, to run once:
  pinMode(pin1, OUTPUT);
  pinMode(pin2, OUTPUT);
  pinMode(pin3, OUTPUT);
  pinMode(pin4, OUTPUT);
  pinMode(ir_pin, INPUT);

  Serial.begin(9600);
  HC05.begin(9600);

}
double T = 310;
void Forward(double distance) {
  double ttime = (distance / ((2 * 3 * PI) / T));
  Serial.println(ttime);
  analogWrite(pin1, 255);
  digitalWrite(pin2, LOW);
  analogWrite(pin3, 255);
  digitalWrite(pin4, LOW);
  delay(ttime);

  digitalWrite(pin1, LOW);
  digitalWrite(pin2, LOW);
  digitalWrite(pin3, LOW);
  digitalWrite(pin4, LOW);
}
void Left(double distance) {
  double ttime = (distance / (2 * 3 * PI / T));
  digitalWrite(pin1, LOW);
  digitalWrite(pin2, LOW);
  digitalWrite(pin3, HIGH);
  digitalWrite(pin4, LOW);
  delay(ttime);
  digitalWrite(pin1, LOW);
  digitalWrite(pin2, LOW);
  digitalWrite(pin3, LOW);
  digitalWrite(pin4, LOW);
}
void Right(double distance) {
  double ttime = (distance / (2 * 3 * PI / T));
  digitalWrite(pin1, HIGH);
  digitalWrite(pin2, LOW);
  digitalWrite(pin3, LOW);
  digitalWrite(pin4, LOW);
  delay(ttime);
  digitalWrite(pin1, LOW);
  digitalWrite(pin2, LOW);
  digitalWrite(pin3, LOW);
  digitalWrite(pin4, LOW);
}
void Back(double distance) {
  double ttime = (distance / (2 * 3 * PI / T));
  digitalWrite(pin1, LOW);
  digitalWrite(pin2, HIGH);
  digitalWrite(pin3, LOW);
  digitalWrite(pin4, HIGH);
  delay(ttime);
  digitalWrite(pin1, LOW);
  digitalWrite(pin2, LOW);
  digitalWrite(pin3, LOW);
  digitalWrite(pin4, LOW);
}
void Turn_180_Right() {
  Right(25);
  Forward(5);
  Right(25);
}
void Turn_90_Right() {
  Right(30);
}
void Turn_90_Left() {
  Left(30);
}
String d = "N";
String Path = "";
bool isread = false;

void loop() {
  //

  // put your main code here, to run repeatedly:
  if (HC05.available() > 0) {
    d = char(HC05.read());
    Serial.println(d);
    Path = HC05.readString();
    Serial.println(Path);
    isread = true;
    String arr[Path.length()] = {};
    int j = 0;
    for (int i = 0; i < Path.length(); i++) {
      if (Path[i] != ' ') {
        arr[j] += Path[i];
      }
      else
        j++;
    }

    for (int i = 0; i <= j; i += 2) {

      double distance = 0;

      for (int k = 0; k < arr[i + 1].length(); k++) {
        distance = distance * 10 + (arr[i + 1][k] - '0');

      }
      Serial.println(arr[i]);
      Serial.println(distance);
      if (d == "N" && arr[i] == "N") {
        Forward(distance);
      }
      else if (d == "N" && arr[i] == "E") {
        d = arr[i];
        Turn_90_Right();
        Forward(distance);
      }
      else if (d == "N" && arr[i] == "W") {
        d = arr[i];
        Turn_90_Left();
        Forward(distance);
      }
      else if (d == "N" && arr[i] == "S") {
        d = arr[i];
        Turn_180_Right();
        Forward(distance);
      }
      else if (d == "E" && arr[i] == "E") {
        d = arr[i];
        Forward(distance);
      }
      else if (d == "E" && arr[i] == "N") {
        d = arr[i];
        Turn_90_Left();
        Forward(distance);
      }
      else if (d == "E" && arr[i] == "S") {
        d = arr[i];
        Turn_90_Right();
        Forward(distance);
      }
      else if (d == "E" && arr[i] == "W") {
        d = arr[i];
        Turn_180_Right();
        Forward(distance);
      }
      else if (d == "S" && arr[i] == "N") {
        d = arr[i];
        Turn_180_Right();
        Forward(distance);
      }
      else if (d == "S" && arr[i] == "E") {
        d = arr[i];
        Turn_90_Left();
        Forward(distance);
      }
      else if (d == "S" && arr[i] == "W") {
        d = arr[i];
        Turn_90_Right();
        Forward(distance);
      }
      else if (d == "S" && arr[i] == "S") {
        d = arr[i];
        Forward(distance);
      }
      else if (d == "W" && arr[i] == "W") {
        d = arr[i];
        Forward(distance);
      }
      else if (d == "W" && arr[i] == "E") {
        d = arr[i];
        Turn_180_Right();
        Forward(distance);
      }
      else if (d == "W" && arr[i] == "N") {
        d = arr[i];
        Turn_90_Right();
        Forward(distance);
      }
      else if (d == "W" && arr[i] == "S") {
        d = arr[i];
        Turn_90_Left();
        Forward(distance);
      }





    }
  }
}






