using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float verticalSpeed = 0f,
        mouseX = 0f,
        mouseY = 0f,
        currentAngle = 0f;
    private CharacterController controller;
    [SerializeField] Camera camera;
    [SerializeField] float mouseSpeed = 50f;

    [SerializeField] GameObject particleObject, tool;
    private const float hitScaleSpeed = 15f;
    private float hitLastTime = 0f;

    private InventoryManager inventoryManager;
    public List<ItemData> inventoryItem, currentChestItems;
    private Transform itemParent;

    bool canMove = true;

    RaycastHit hit;
    public const string EQUIPE_NOT_SELECTED_TEXT = "EquipeNotSelected";
    [HideInInspector]
    public string itemYouCanEquipName = EQUIPE_NOT_SELECTED_TEXT;
    [SerializeField] GameObject[] equipabletItems;
    GameObject currentEquipedItem;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        inventoryManager = GameObject.Find("InventoryManager")
        .GetComponent<InventoryManager>();
        itemParent = GameObject.Find("InventoryContent").transform;
        inventoryManager.CreateItem(0, inventoryItem);
    }

    void Start()
    {
        EquipItem("Sword");

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            RotateCharacter();
            MoveCharacter();

            if (Physics.Raycast(camera.transform.position, camera.transform.forward
                , out hit, 5f))
            {
                if (Input.GetMouseButton(0))
                {
                    ObjectInteraction(hit.transform.gameObject);
                }
            }
            
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inventoryManager.inventoryPanel.activeSelf)
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.E) && inventoryManager.inventoryPanel.activeSelf)
        {
            CloseInventoryPanels();
        }
        else if(Input.GetMouseButtonDown(1) && itemYouCanEquipName != EQUIPE_NOT_SELECTED_TEXT && inventoryManager.inventoryPanel.activeSelf)
            {
                EquipItem(itemYouCanEquipName);
        }
    }

    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0f, mouseX * Time.fixedDeltaTime * mouseSpeed, 0f));
        currentAngle += mouseY * Time.fixedDeltaTime * -mouseSpeed;
        currentAngle = Mathf.Clamp(currentAngle, -60f, 60f);
        camera.transform.localEulerAngles = new Vector3(currentAngle, 0f, 0f);
    }

    private void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * 5f;
        if (controller.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetButton("Jump")) verticalSpeed = 8f;
        }
        verticalSpeed -= 9.81f * Time.fixedDeltaTime;
        velocity.y = verticalSpeed;
        controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void Dig(Block block)
    {
        if (Time.time - hitLastTime > 1 / hitScaleSpeed)
        {
            tool.GetComponent<Animator>().SetTrigger("attack");
            hitLastTime = Time.time;
            Tool currentToolInfo;
            if(tool.TryGetComponent<Tool>(out currentToolInfo))
            {
                block.health -= currentToolInfo.damageToBlocks;
            }
            else
            {
                block.health -= 1;
            }
            block.health -= tool.GetComponent<Tool>().damageToBlocks;
            GameObject go = Instantiate(particleObject,
                block.gameObject.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material =
                block.GetComponent<MeshRenderer>().material;
            if (block.health <= 0)
            {
                block.DestroyBehaviour();
            }
        }
    }

    private void EquipItem(string toolName)
    {
        foreach(GameObject Tool in equipabletItems)
        {
            if(toolName == Tool.name)
            {
                Tool.SetActive(true);
                tool = Tool;
                toolName = EQUIPE_NOT_SELECTED_TEXT;
            }
            else
            {
                Tool.SetActive(false);
            }
        }
    }

    private void ObjectInteraction(GameObject tempObject)
    {
        switch (tempObject.tag)
        {
            case "Block":
                Dig(tempObject.GetComponent<Block>());
                break;
            case "Enemy":
                break;
            case "Chest":
                currentChestItems = tempObject.GetComponent<Chest>().chestItems;
                OpenChest();
                break;
        }
    }

    private void OpenInventory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        inventoryManager.inventoryPanel.SetActive(true);
        
        if (inventoryItem.Count > 0)
        {
            for (int i = 0; i < inventoryItem.Count; i++)
            {
                inventoryManager.InstantingItem(inventoryItem[i],
                    itemParent,
                    inventoryManager.inventorySlots);
            }
        }
        canMove = false;
    }
    
    private void OpenChest()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        inventoryManager.chestPanel.SetActive(true);
        Transform chestParent = GameObject.Find("ChestContent").transform;
        if (currentChestItems.Count > 0)
        {
            for (int i = 0; i < currentChestItems.Count; i++)
            {
                inventoryManager.InstantingItem(currentChestItems[i],
                    chestParent,
                    inventoryManager.chestSlots);
            }
        }
        canMove = false;
    }

    private void CloseInventoryPanels()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (GameObject slot in inventoryManager.inventorySlots)
        {
            Destroy(slot);
        }
        inventoryManager.inventorySlots.Clear();
        inventoryManager.inventoryPanel.SetActive(false);
        
        foreach (GameObject slot in inventoryManager.chestSlots)
        {
            Destroy(slot);
        }
        inventoryManager.chestSlots.Clear();
        inventoryManager.chestPanel.SetActive(false);

        canMove = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("mini"))
        {
            inventoryManager.CreateItem(2, inventoryItem);
            Destroy(other.gameObject);
        }
    }
}
