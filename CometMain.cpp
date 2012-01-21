#include "CometMain.hpp"
#include "Unit4.h"
#include "Unit3.h"


 double compute_period (double q, double e){

	double P = 0.0;

	if (e <  1.000000) P = pow((q/(1.0-e)),1.5);
	if (e >  1.000000) P = pow((q/(e-1.0)),1.5);
	if (e == 1.000000) P = pow((q/(1-0.999999)),1.5);

	return P;
}

long int greg_to_jul (int y, int m, int d){

	return 367*y - (7*(y + (m + 9)/12))/4 - ((3*(y + (m - 9)/7))/100 + 1)/4 + (275*m)/9 + d + 1721029;
}

void remove_spaces (char *name){

	int len = strlen(name);

	//for (int i=0; i<len-1; i++)
	for (int i=0; i<len; i++)
		//if (name[i]==' ' && name[i+1]==' ')
		if (name[i]==' ' && (name[i+1]==' ' || name[i+1]=='\0')){
			name[i]='\0';
			break;
		}
}

void editFullIdName(char *full, char *ID, char *name, int type){

	int j, k, l;

	if(type == 1){
		for (j=0; full[j]!='\0'; j++){
			if ((isdigit(full[j]) && full[j+1]=='P'
				//&& full[j+2]=='/'
				) ||
				(isdigit(full[j]) && full[j+1]=='D'
				//&& full[j+2]=='/'
				)){

				for(k=0; full[k]!='/'; k++)
					ID[k]=full[k];

				ID[k]='\0';
				++k;
				for(l=0; full[k]!='\0'; l++, k++)
					name[l]=full[k];

				name[l]='\0';
				break;
			}

			if (full[j]=='('){
				for(k=0; full[k]!='('; k++)
					ID[k]=full[k];

				ID[k-1]='\0';

				++k;
				for(l=0; full[k]!=')'; k++, l++)
					name[l]=full[k];

				name[l]='\0';
				break;
			}
		}
	}


}

double get_sort_key(char *ID){

	int k;
	double sort, v=0.0;
	char temp[4+1], tempp[2+1], temppp[3+1];

	temp[0]='\0';
	temp[1]='\0';
	temp[2]='\0';
	temp[3]='\0';
	temp[4]='\0';

	tempp[0]='\0';
	tempp[1]='\0';
	tempp[2]='\0';

	temppp[0]='\0';
	temppp[1]='\0';
	temppp[2]='\0';
	temppp[3]='\0';

	if(isdigit(ID[0])){
		k=0;
		do{
			temp[k]=ID[k];
		}while(isdigit(ID[k++]));

		sort = atof(temp);
	}

	else {

		temp[0]=ID[2];
		temp[1]=ID[3];
		temp[2]=ID[4];
		temp[3]=ID[5];

		sort = atof(temp);

		k=7;

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && ID[k+2]=='\0'){

			v = (ID[k]-64)/(double)100;
			v += (ID[k]-64)/(double)10000;
		}

		if (isalpha(ID[k]) && isdigit(ID[k+1]) && ID[k+2]=='\0'){

			v = (ID[k]-64)/(double)100;
			v += (double)(ID[k+1]-48)/100000;
		}

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && isdigit(ID[k+2]) && ID[k+3]=='\0'){

			v = (ID[k]-64)/(double)100;
			v += (ID[k+1]-64)/(double)10000;
			v += (ID[k+2]-48)/(double)100000;
		}

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && isdigit(ID[k+2]) && isdigit(ID[k+3])  && ID[k+4]=='\0'){

			v = (ID[k]-64)/(double)100;
			v += (ID[k+1]-64)/(double)10000;

			tempp[0]=ID[k+2];
			tempp[1]=ID[k+3];

			v += atof(tempp)/(double)1000000;
		}

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && isdigit(ID[k+2]) && isdigit(ID[k+3]) && isdigit(ID[k+4])){

			v = (ID[k]-64)/(double)100;
			v += (ID[k+1]-64)/(double)10000;

			temppp[0]=ID[k+2];
			temppp[1]=ID[k+3];
			temppp[2]=ID[k+4];

			v += atof(temppp)/(double)10000000;
		}

		sort+=v;
	}

	return sort;
}

void jul_to_greg (long int T, int &y, int &m, float &d){

//	izracuvanje gregorijanskog datuma iz julijanskog dana
//	izvor: http://en.wikipedia.org/wiki/Julian_day#Gregorian_calendar_from_Julian_day_number

	int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	v1 = T + 0.5;
	v2 = v1 + 32044;
	v3 = v2 / 146097;
	v4 = v2 % 146097;
	v5 = (v4 / 36524 + 1) * 3 / 4;
	v6 = v4 - v5 * 36524;
	v7 = v6 / 1461;
	v8 = v6 % 1461;
	v9 = (v8 / 365 + 1) * 3 / 4;
	v10 = v8 - v9 * 365;
	v12 = v3 * 400 + v5 * 100 + v7 * 4 + v9;
	v11 = (v10 * 5 + 308) / 153 - 2;
	v13 = v10 - (v11 + 4) * 153 / 5 + 122;
	y = v12 - 4800 + (v11 + 2) / 12;
	m = (v11 + 2) % 12 + 1;
	d = v13 + 1;
}

void wait ( int seconds ){
	clock_t endwait;
	endwait = clock () + seconds * CLOCKS_PER_SEC ;
	while (clock() < endwait) {}
}
