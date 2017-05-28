using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Settings")]
public class XMLSerializer
{
    [XmlAttribute("Version_Number")]
    public float versionNumber;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializer));
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            serializer.Serialize(stream, this);
    }

    public static XMLSerializer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializer));
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            return serializer.Deserialize(stream) as XMLSerializer;

    }
}

[XmlRoot("Collection_Of_Pokemon")]
public class XMLSerializerPokemon
{
    [XmlArray("Pokemon"), XmlArrayItem("Pokemon")]
    public List<Pokemon> pokemon = new List<Pokemon>();

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializerPokemon));
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            serializer.Serialize(stream, this);
    }

    public static XMLSerializerPokemon Load(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializerPokemon));
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            return serializer.Deserialize(stream) as XMLSerializerPokemon;

    }
}

[XmlRoot("Collection_Of_Items")]
public class XMLSerializerItems
{
    [XmlArray("Items"), XmlArrayItem("Item")]
    public List<Items> items = new List<Items>();

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializerItems));
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            serializer.Serialize(stream, this);
    }

    public static XMLSerializerItems Load(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializerItems));
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            return serializer.Deserialize(stream) as XMLSerializerItems;
    }
}

[XmlRoot("Collection_Of_Moves")]
public class XMLSerializerMoves
{
    [XmlArray("Moves"), XmlArrayItem("Move")]
    public List<Move> moves = new List<Move>();

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializerMoves));
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            serializer.Serialize(stream, this);
    }

    public static XMLSerializerMoves Load(string path)
    {
        var serializer = new XmlSerializer(typeof(XMLSerializerMoves));
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            return serializer.Deserialize(stream) as XMLSerializerMoves;
    }
}

[Serializable]
public class Pokemon
{
    public enum Type
    {
        fire,
        water,
        poison,
        steel,
        grass,
        bug,
        dark,
        flying,
        ghost,
        dragon,
        electric,
        fighting,
        ground,
        ice,
        normal,
        psychic,
        rock,
        fairy
    }

    public enum Status
    {
        poison,
        burnt,
        dead,
        sleep,
        paralysis,
        freeze
    }

    [XmlAttribute("Pokemon_Types")]
    public List<Type> pTypes;
    [XmlAttribute("Pokemon_Name")]
    public string pName;
    [XmlAttribute("Pokemon_Moves_Names")]
    public List<string> pMoves_Names;
    [XmlAttribute("Pokemon_Moves")]
    public List<Move> pMoves;
    [XmlAttribute("Pokemon_Speed")]
    public int pSpeed;
    [XmlAttribute("Pokemon_MaxHP")]
    public int pMaxHp;
    [XmlAttribute("Pokemon_HP")]
    public int pHp;
    [XmlAttribute("Pokemon_Status")]
    public List<Status> pStatus;

    public Pokemon(string name, int speed, int maxHp, int hp, Type[] types = null, Status[] status = null, string[] movesNames = null)
    {
        if (types == null)
            pTypes = new List<Type>();
        else
            pTypes = types.ToList();
        if (movesNames == null)
            pMoves_Names = new List<string>();
        else
            pMoves_Names = movesNames.ToList();
        if (status == null)
            pStatus = new List<Status>();
        else
            pStatus = status.ToList();

        pName = name;
        pSpeed = speed;
        pMaxHp = maxHp;
        pHp = hp;
    }

    public Pokemon ()
    {
        pTypes = new List<Type>();
        pMoves = new List<Move>();
        pMoves_Names = new List<string>();
        pStatus = new List<Status>();
    }
}

[Serializable]
public class Move
{
    [XmlAttribute("Move_Damage")]
    public int mDamage;
    [XmlAttribute("Move_Target")]
    public Battler.Battlers mTarget;
    [XmlAttribute("Move_Type")]
    public Pokemon.Type mType;
    [XmlAttribute("Move_Name")]
    public string mName;
    [XmlAttribute("Move_Chance")]
    public float mChance;
    [XmlAttribute("Move_Status_Remove")]
    public List<Pokemon.Status> mStatusRemove;
    [XmlAttribute("Move_Status_Add")]
    public List<Pokemon.Status> mStatusAdd;

    public Move(int dmg, Pokemon.Type type, string name, float chance, Battler.Battlers target, Pokemon.Status[] statusAdd = null, Pokemon.Status[] statusRemove = null)
    {
        mDamage = dmg;
        mTarget = target;
        mType = type;
        mName = name;
        mChance = chance;
        if (statusAdd != null)
            mStatusAdd = statusAdd.ToList();
        else
            mStatusAdd = new List<Pokemon.Status>();

        if (statusRemove != null)
            mStatusRemove = statusRemove.ToList();
        else
            mStatusRemove = new List<Pokemon.Status>();
    }

    public Move()
    {
        mStatusAdd = new List<Pokemon.Status>();
        mStatusRemove = new List<Pokemon.Status>();
    }
}

[Serializable]
public class Items
{
    [Serializable]
    public class StatChange
    {
        [XmlAttribute("Stat_Change")]
        public Stat statChange;
        [XmlAttribute("Stat_Value")]
        public int valueChange;

        public StatChange() { }

        public StatChange(Stat stat, int value)
        {
            statChange = stat;
            valueChange = value;
        }
    }

    [XmlAttribute("Item_Type")]
    public List<Type> iType;
    [XmlAttribute("Item_Name")]
    public string iName;
    [XmlAttribute("Item_Healing")]
    public int iAmountHealing; //Only For Healing
    [XmlAttribute("Item_Status_Remove")]
    public List<Pokemon.Status> iStatusRemove; //Only for status
    [XmlAttribute("Item_Status_Add")]
    public List<Pokemon.Status> iStatusAdd; //Only for status
    [XmlArray("StatChanges"), XmlArrayItem("StatChange")]
    public List<StatChange> iStatChanges; //Only for stat changes

    public enum Stat
    {
        speed,
        hp,
        maxHp
    }

    public enum Type
    {
        healing,
        statusHealer,
        statChanger
    }

    public Items(string name, Type[] types = null, int amountHealing = 0, Pokemon.Status[] statusAdd = null, Pokemon.Status[] statusRemove = null, StatChange[] statChanges = null)
    {
        iName = name;
        iAmountHealing = amountHealing;

        if (types != null)
            iType = types.ToList();
        else
            iType = new List<Type>();

        if (statusAdd != null)
            iStatusAdd = statusAdd.ToList();
        else
            iStatusAdd = new List<Pokemon.Status>();

        if (statusRemove != null)
            iStatusRemove = statusRemove.ToList();
        else
            iStatusRemove = new List<Pokemon.Status>();

        if (statChanges != null)
            iStatChanges = statChanges.ToList();
        else
            iStatChanges = new List<StatChange>();
    }

    public Items()
    {
        iStatChanges = new List<StatChange>();
        iStatusAdd = new List<Pokemon.Status>();
        iStatusRemove = new List<Pokemon.Status>();
        iType = new List<Type>();
    }
}