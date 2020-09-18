//Using the size of the map to determine when to change "wall" is the method to go!

const API = require('./API');
function log(text) {
    console.error(text);
}

let noX = [];
let noY = [];
var lenght = 0;
var pos = [0,0];
var start = [0,0];
let turn = 0;
//Winning pos = 7,7 / 7,8 / 8,7 / 8,8
var direction = ['n', 'e', 's', 'w'];
var dir_current = 0;
var endgame = false;

function change_dir(num){
	if(num == -1) if(dir_current == 0){
		dir_current = 3;
		return;
	}
	if(num == 1) if(dir_current == 3){
		dir_current = 0;
		return;
	}
	dir_current += num;
}

function change_pos(){
	if(direction[dir_current] == 'n')
		pos[1]++;
	else if(direction[dir_current] == 'e')
		pos[0]++;
	else if(direction[dir_current] == 's')
		pos[1]--;
	else if(direction[dir_current] == 'w')
		pos[0]--;
	else
		log('something went terribly wrong');
}

async function main() {
    log("Running...");
    API.setColor(0, 0, 'G');
    API.setText(0, 0, "abc");
    while (!endgame){
		while(turn < 32 && !endgame){
			while(await wallLeft() && !await wallFront()) forward();
			if(!await wallLeft()){
				left();
				forward();
			}
			else if(!await wallRight()){
				right();
				forward();
			}
			else deadEnd();
			endgame = checkEnd();
			turn++;
			log('turn = ' + turn);
		}
		while(turn >= 32 && !endgame){
			while(await wallRight() && !await wallFront()) forward();
			if(!await wallRight()){
				right();
				forward();
			}
			else if(!await wallLeft()){
				left();
				forward();
			}
			else deadEnd();
			endgame = checkEnd();
			turn++;
			log('turn = ' + turn);
			if(turn == 64) turn = 0;
		}
    }
	log('winner winner chicken dinner');
}

//Remove mouse from beginning area only.
function begin(){
	while(API.wallRight()){
		if(!API.wallRight()){
			right();
			forward();
			return;
		}
		forward();
	}
}

function left(){
	API.turnLeft();
	change_dir(-1);
}

function right(){
	API.turnRight();
	change_dir(1);
}

function forward(){
	API.moveForward();
	change_pos();
}

function placeWall(){
	API.setWall(pos[0], pos[1], direction[dir_current]);
	noX.push(pos[0]);
	noY.push(pos[1]);
	lenght++;
}

function deadEnd(){
	left();
	left();
	placeWall();
	forward();
}


async function wallLeft(){
	if(!API.wallLeft()){
		if(lenght != 0)
			for (let assist = 0; assist < lenght; assist++) {
				if(direction[dir_current] == 'n'){
					if(pos[0]-1 == noX[assist] && pos[1] == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 'e'){
					if(pos[0] == noX[assist] && pos[1]+1 == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 's'){
					if(pos[0]+1 == noX[assist] && pos[1] == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 'w'){
					if(pos[0] == noX[assist] && pos[1]-1 == noY[assist]){
						return true;
					}
				}
				else{
					log('Something went wrong');
					return true;
				}
			}
		return false;	
	}
	else return true;
}

async function wallRight(){
	if(!API.wallRight()){
		if(lenght != 0)
			for (let assist = 0; assist < lenght; assist++) {
				if(direction[dir_current] == 'n'){
					if(pos[0]+1 == noX[assist] && pos[1] == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 'e'){
					if(pos[0] == noX[assist] && pos[1]-1 == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 's'){
					if(pos[0]-1 == noX[assist] && pos[1] == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 'w'){
					if(pos[0] == noX[assist] && pos[1]+1 == noY[assist]){
						return true;
					}
				}
				else{
					log('Something went wrong');
					return true;
				}
			}
		return false;
	}
	else return true;
}

async function wallFront(){
	if(!API.wallFront()){
		if(lenght != 0)
			for (let assist = 0; assist < lenght; assist++) {
				if(direction[dir_current] == 'n'){
					if(pos[0] == noX[assist] && pos[1]+1 == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 'e'){
					if(pos[0]+1 == noX[assist] && pos[1] == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 's'){
					if(pos[0] == noX[assist] && pos[1]-1 == noY[assist]){
						return true;
					}
				}
				else if (direction[dir_current] == 'w'){
					if(pos[0]-1 == noX[assist] && pos[1] == noY[assist]){
						return true;
					}
				}
				else{
					log('Something went wrong');
					return true;
				}
			}
		return false;
	}
	else return true;
}

function checkEnd(){
	if(pos[0] == 7 || pos[0] == 8)
		if(pos[1] == 7 || pos[1] == 8)
			return true;
	return false;
}

main();
