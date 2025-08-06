CREATE TABLE "Tarefas" (
    "Id" UUID PRIMARY KEY,
    "Titulo" VARCHAR(200) NOT NULL,
    "Descricao" TEXT NULL,
    "Status" INT NOT NULL,
    "DataCriacao" TIMESTAMP NOT NULL,
    "DataConclusao" TIMESTAMP NULL
);