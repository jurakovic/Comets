int TForm1::import_cfw (int N, FILE *fin){

	int j, k, l;
	float G;

	//for (int i=0; i<7; i++) fscanf(fin, "%*[^\n]\n");

	fscanf(fin, "\n");
	fscanf(fin, "[File]\n");
	fscanf(fin, "group=Comets\n");
	fscanf(fin, "\n");
	fscanf(fin, "\n");
	fscanf(fin, "[Data]\n");
	fscanf(fin, "\n");

	for (int i=0; i<N; i++){

		Comet *com = new Comet;

		fscanf(fin, "name=%40[^\n]%*c\
					%*[^\n]\n\
					type=orbit\n\
					T=%d %d %f\n\
					q=%f\n\
					e=%f\n\
					peri=%f\n\
					node=%f\n\
					i=%f\n\
					prec=2000.0\n\
					%*[^\n]\n\
					mageq=%f %f\
					\n",
					com->full,
					&com->y, &com->m, &com->d,
					&com->q,
					&com->e,
					&com->pn,
					&com->an,
					&com->i,
					&com->H, &G);

		com->G = G/2.5;

		remove_spaces(com->full);

		for (j=0; com->full[j]!='\0'; j++){

			if (isdigit(com->full[j]) && com->full[j+1]=='P' && com->full[j+2]=='/'){

				for(k=0; com->full[k]!='/'; k++)
					com->ID[k]=com->full[k];

				com->ID[k]='\0';

				++k;

				for(l=0; com->full[k]!='\0'; l++, k++)
					com->name[l]=com->full[k];

				com->name[l]='\0';
				break;
			}

			if (com->full[j]=='('){
				for(k=0; com->full[k]!='('; k++)
					com->ID[k]=com->full[k];

				com->ID[k-1]='\0';

				++k;

				for(l=0; com->full[k]!=')'; k++, l++)
					com->name[l]=com->full[k];

				com->name[l]='\0';
				break;
			}
		}

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		Frame31->ProgressBar1->Position = i+1;

		if(Frame21->CheckBox1->Checked && do_exclude(com)){
			N--;
			i--;
			Frame31->ProgressBar1->Max--;
			delete com;
			continue;
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}