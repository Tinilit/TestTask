export class ProductListRequest {
    public page: number;
    public term: string;
    public categoryFilterId: string;
}

export class ProductListResponse {

    public values: Array<ProductDto>;
    public total: number;
}

export class ProductDto {
    public id: string;
    public title: string;
    public price: number;
    public createdDate: Date;
    public categoryId: string;
    public categoryTitle: string;
}