<?php

class {TABLE.NAME}{
	var $tablename;
	var $conn;
	{TABLE.COLUMNS}
	var ${COLUMN.NAME};{/TABLE.COLUMNS}
		
	function {TABLE.NAME}($conn,$tablename='{TABLE.NAME}'){
		$this->conn = $conn;		
		$this->tablename = $tablename;				
	}
	
	function getList(){
		$sqlcommand = "SELECT {TABLE.COLUMNS}{COLUMN.NAME}{IF NOT LAST}, {/IF}{/TABLE.COLUMNS} FROM ".$this->tablename;
        
        $rs = $this->conn->Execute($sqlcommand);
		print $this->conn->ErrorMsg();
        if(!$rs){
            trigger_error($this->conn->ErrorNo().": ".$this->conn->ErrorMsg(),E_USER_WARNING);
            return false;
        }
        else{
            return $rs->getArray();
        }
	}
	
	function insert(){
		$sqlcommand = "INSERT INTO ".$this->tablename." ({TABLE.COLUMNS NOPRIMARY}{COLUMN.NAME}{IF NOT LAST},{/IF}{/TABLE.COLUMNS}) VALUES ({TABLE.COLUMNS NOPRIMARY}'$this->{COLUMN.NAME}'{IF NOT LAST},{/IF}{/TABLE.COLUMNS})";
		if(!$this->conn->Execute($sqlcommand))
            trigger_error($this->conn->ErrorNo().": ".$this->conn->ErrorMsg(),E_USER_WARNING);
        return $this->conn->Insert_ID();		
	}
	
	function load(){
		$sqlcommand = "SELECT {TABLE.COLUMNS}{COLUMN.NAME}{IF NOT LAST}, {/IF}{/TABLE.COLUMNS} FROM ".$this->tablename." WHERE {TABLE.COLUMNS PRIMARY}{COLUMN.NAME}=$this->{COLUMN.NAME}{/TABLE.COLUMNS}";		
        $rs = $this->conn->Execute($sqlcommand);
        if(!$rs)
            trigger_error($this->conn->ErrorNo().": ".$this->conn->ErrorMsg(),E_USER_WARNING);
        ${TABLE.NAME} = $rs->FetchRow();
        {TABLE.COLUMNS}
        $this->{COLUMN.NAME} = ${TABLE.NAME}['{COLUMN.NAME}'];{/TABLE.COLUMNS}		
	}
	
	function update(){
		$sqlcommand = "UPDATE ".$this->tablename." SET {TABLE.COLUMNS NOPRIMARY}{COLUMN.NAME} = '$this->{COLUMN.NAME}'{IF NOT LAST},{/IF}{/TABLE.COLUMNS} WHERE {TABLE.COLUMNS PRIMARY}{COLUMN.NAME}= $this->{COLUMN.NAME}{/TABLE.COLUMNS}";
		if(!$this->conn->Execute($sqlcommand))
            trigger_error($this->conn->ErrorNo().": ".$this->conn->ErrorMsg(),E_USER_WARNING);
        return true;
		
	}
	
	function delete(){
		$sqlcommand = "DELETE FROM ".$this->tablename." WHERE {TABLE.COLUMNS PRIMARY}{COLUMN.NAME} = $this->{COLUMN.NAME}{/TABLE.COLUMNS}";
		if(!$this->conn->Execute($sqlcommand)){
            trigger_error($this->conn->ErrorNo().": ".$this->conn->ErrorMsg(),E_USER_WARNING);
		}
	}
}
?>