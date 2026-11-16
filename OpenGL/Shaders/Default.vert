#version 460 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aCol;

uniform vec3 offset;

out vec4 aColour;


//layout (location = 0) in vec3 bPos;

void main(){

	//gl_Position = vec4(aPos, 1.0f);
	gl_Position = vec4(aPos.x + offset.x, aPos.y + offset.y, aPos.z, 1.0f);

	aColour = vec4(aCol, 1.0f);

	//gl_Position = vec4(bPos.x, bPos.y, bPos.z, 1.0f);
}
