#include <cstdio>
#include <cstdlib>

class list{

	public:
	int a;
	list *next;

	list(){
		a = 0;
		next = NULL;
	}
};

void dodaj(list **l, int value){

	if(*l==NULL){

		list *temp = new list;
		temp->a = value;
		temp->next = NULL;
		*l = temp;
	}

	else{
		list *temp = *l;

		while(temp->next != NULL){
			temp = temp->next;
		}

		list *r = new list;
		r->a = value;
		r->next = NULL;
		temp->next = r;
	}
}

list *sortiraj(list *l){

	list *sorted = NULL;

	while(l!=NULL){

		list *head = l;
		list **trail = &sorted;

		l = l->next;

		while(1){

			if(*trail == NULL || head->a < (*trail)->a){
				head->next = *trail;
				*trail = head;
				break;
			}
			else{
				trail = &(*trail)->next;
			}
		}

	}

	return sorted;
}

void ispisi(list *head){

	while(head!=NULL){
		printf("%d ", head->a);
		head = head->next;
	}
	printf("\n\n");
}

void ocisti(list **l){

	list *current = NULL;
	while ((*l) != NULL){
        current = *l;
        *l = current->next;
        free(current);
    }
}



int main(){

	list *a = NULL;

	dodaj(&a, 1);
	dodaj(&a, 9);
	dodaj(&a, 3);
	dodaj(&a, 5);
	dodaj(&a, 2);
	dodaj(&a, 7);

	ispisi(a);

	a = sortiraj(a);

	ispisi(a);

	ocisti(&a);

	return 0;
}
