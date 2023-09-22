#define input "D:/Github homwork/btap.txt"
#include <stdio.h>
#include <stdlib.h>
#define SIZE 50

void docDoThi(int A[][SIZE], int &n);
void toMau(int A[][SIZE],int n,int Mau[]);
void xuat(int A[], int n);

int main(){
    int A[SIZE][SIZE], n;
    int Mau[SIZE];
    docDoThi(A,n);
    printf("Nguyen Thanh Tam-215052015\n");
    for(int i =0; i<n; i++){
        for(int j=0; j<n;j++){
        printf("%d ", A[i][j]);
        }
    printf("\n");    
    }
    
    printf("\nDo thi co mau:");
    printf("\nA B C D E F G Z \n");
    toMau(A,n,Mau);
    xuat(Mau,n);
    return 1;
}


void docDoThi(int A[][SIZE], int &n){
    FILE *fi = fopen(input,"rt");
    if(fi == NULL){
        printf("Khong mo duoc file!");
        exit(0);
    }
    fscanf(fi,"%d ",&n);
    for(int i =0; i<n; i++){
        for(int j=0; j<n;j++){
        fscanf(fi,"%d ", &A[i][j]);
        }
    }fclose(fi);
}

void toMau(int A[][SIZE],int n,int Mau[]){
    int BangMau[SIZE][SIZE] = {0};
    int mauvuato;
    for(int i=0; i<n; i++){
        for(int k = 0; k <n; k++)
            if(BangMau[i][k] == 0){
                Mau[i] = k;
                mauvuato = k;
                break;
        }
        for(int j=i+1; j<n; j++){
            if(A[i][j] > 0)
                BangMau[j][mauvuato] = 1;
        }
    }
}

void xuat(int A[], int n){
    for(int i=0;i<n;i++){
        printf("%d ",A[i] + 1);
    }
}