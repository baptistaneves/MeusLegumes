export class Produto {
    id: string;
    categoriaId: string;
    unidadeId: string;
    impostoId: string;
    motivoId: string;
    nome: string;
    descricao: string;
    precoUnitario: number
    urlImagemPrincipal: string;
    imagemUpload: string;
    emPromocao: boolean;
    precoPromocional: number;
    destaque: boolean;
    novoLancamento: boolean;
    maisVendido: boolean;
    maisProcurado: boolean;
    emEstoque: boolean;
    activo: boolean;
    observacao: string;

}