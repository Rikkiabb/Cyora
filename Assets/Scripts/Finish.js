#pragma strict


function OnTriggerEnter2D (obj : Collider2D){
	
	if(obj.name == "Player"){
		
//		var cam : CameraPosition = GetComponent(CameraPosition);
//		cam.target = camera.transform;
		//TODO: Disable controll script
		CameraPosition.target = transform;
		//TODO: PLay animation
	
	}

}