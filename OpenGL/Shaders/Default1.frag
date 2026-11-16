#version 460 core

out vec4 fColour;
uniform vec4 Default3;
in vec4 aColour;

//out vec4 fColour;

//bvec4 boolean
//uvec4 usign integers
// ivec4 int 
// dvec4 prieks tabulam

void main(){
	//fColour = vec4(0.3f, 0.3f, 0.6f, 0.8f);
	//fColour = Default3;
	fColour = aColour;
	//fColour = vec4(0.6f, 0.3f, 0.6f, 0.8f);
}