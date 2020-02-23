
update S_A_Org set FullId=''

Declare @RootId nvarchar(36)  
Set @RootId = 'a1b10168-61a9-44b5-92ca-c5659456deb5'; ---在此修改父节点  



With RootNodeCTEee(Id,ParentId,FullId)  
As  
(  
    Select Id,ParentId,FullId From S_A_Org Where Id In (@RootId)  
    Union All  
    Select S_A_Org.Id,S_A_Org.ParentId,S_A_Org.FullId From RootNodeCTEee  
        Inner Join S_A_Org
        On RootNodeCTEee.Id = S_A_Org.ParentId
)
select * into #T from RootNodeCTEee
--select * from #T 
declare @Id varchar(36) --
declare @ParentId varchar(36) --
declare @FullId varchar(512) --
declare @TempORG_PARENT_PATH varchar(512) --
Declare row Cursor For    --声明游标row 
Select Id,ParentId,FullId From #T--
--select * from RootNodeCTEee 

Open row   
Fetch Next From row into @Id,@ParentId,@FullId   --
Fetch Next From row into @Id,@ParentId,@FullId   --
While @@FETCH_STATUS = 0              --完成状态
begin
print(@Id+'__'+@ParentId+'__'+@FullId)
select @TempORG_PARENT_PATH= FullId + '.'+Id from S_A_Org where Id=@ParentId  
update S_A_Org set FullId = @TempORG_PARENT_PATH where Id = @Id 

Fetch Next From row into @Id,@ParentId,@FullId 

end
Close row
Deallocate row 

drop table #T

update S_A_Org set FullId=FullId+'.'+Id
update S_A_Org set FullId=substring(FullId,2,len(FullId)-1)

--update S_A_Org set FullID='.'+@RootId where Id=@RootId;
--update S_A_Org set

select ID,ParentID,FullID from S_A_Org